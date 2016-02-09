using Northernrunners.ImportLibrary.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Datalayer;

namespace NR_Resultat_Import
{
    public partial class Form2 : Form
    {
        private ICollection<UserEventInfo> _deltakere;
        private readonly EventResultHandler _eventResultHandler;
        public Form2(ICollection<UserEventInfo> deltakere, EventResultHandler handler)
        {
            _eventResultHandler = handler;
            
            InitializeComponent();
            this._deltakere = deltakere;
            var bindingList = new BindingList<UserEventInfo>(deltakere.ToList());
            var source = new BindingSource(bindingList,null);
            dataGridView1.DataSource = source;
            dataGridView1.ReadOnly = false;
            if (deltakere.First() != null)
            {
                //var x = new MockedEventService();
                //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
                //List<Event> eventer = eventService.GetEvents("adsf", DateTime.Now.Year).ToList();
                var eventer = _eventResultHandler.GetEvents(new DateTime(2015, 5, 10), new DateTime(2015, 10, 31)); //DateTime.Now
                if (eventer.Count >  0)
                {
                    label2.Text = $"ID={eventer.First().Id}";
                    listBox1.Items.Clear();
                    listBox1.DataSource = eventer;
                    listBox1.DisplayMember = "DisplayName";
                    listBox1.ValueMember = "Id";
                }
                else
                {
                    label2.Text = "Velg event...";
                    listBox1.Items.Clear();
                    listBox1.DataSource = eventer;
                    listBox1.DisplayMember = "DisplayName";
                    listBox1.ValueMember = "Id";
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = (ListBox) sender;
            var ev = (Event) x.SelectedItem;
            label2.Text = ev.DisplayName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _eventResultHandler.LoadAndFillDeltakerInfo(_deltakere);
        }

        private void btnSubmitResults_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmitResults.Enabled = false;
                this._deltakere = (ICollection<UserEventInfo>)((BindingSource)dataGridView1.DataSource).List;
                Cursor.Current = Cursors.WaitCursor;
                Event ev = (Event)listBox1.SelectedItem;
                this.backgroundWorker1.RunWorkerAsync(ev);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private List<string> CheckValidData(ICollection<UserEventInfo> collection)
        {
            var list = new List<string>();
            foreach (var deltaker in collection.Where(deltaker => !list.Contains(deltaker.Stage)))
            {
                list.Add(deltaker.Stage);
            }
            return list;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var valid = CheckValidData(_deltakere);

            if (valid.Count == 1)
            {
                var ev = (Event)e.Argument;
                //var ev = (Event)listBox1.SelectedItem;
                _eventResultHandler.InsertResultInEvent(this._deltakere, ev);
            }
            else
            {
                string stages = valid.Aggregate("", (current, stage) => current + stage + " - ");
                var message = "Inneholder flere events.. " + stages;
                MessageBox.Show(message, @"Feil i fil", MessageBoxButtons.OK);
            }
        }
    }
}
