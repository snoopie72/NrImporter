using Northernrunners.ImportLibrary.Service.Mocked;
using Northernrunners.ImportLibrary.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Datalayer;
using Northernrunners.ImportLibrary.Utils;

namespace NR_Resultat_Import
{
    public partial class Form2 : Form
    {
        private readonly ICollection<UserEventInfo> _deltakere;
        private readonly EventResultHandler _eventResultHandler;
        public Form2(ICollection<UserEventInfo> deltakere)
        {
            var sqlDirectService = new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            _eventResultHandler = new EventResultHandler(new UserService(new ResultDataService(sqlDirectService)), new EventService(new ResultDataService(sqlDirectService)));
            
            InitializeComponent();
            this._deltakere = deltakere;
            var bindingList = new BindingList<UserEventInfo>(deltakere.ToList());
            var source = new BindingSource(bindingList,null);
            dataGridView1.DataSource = source;
            dataGridView1.ReadOnly = false;
            if (deltakere.First() != null)
            {
                //var x = new MockedEventService();
                var eventService = new EventService(new ResultDataService(sqlDirectService));
                //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
                //List<Event> eventer = eventService.GetEvents("adsf", DateTime.Now.Year).ToList();
                var eventer = eventService.GetEvents(new DateTime(2012, 1, 1), DateTime.Now);
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
            var valid = CheckValidData(_deltakere);

            if (valid.Count == 1)
            {
                var ev = (Event) listBox1.SelectedItem;
                _eventResultHandler.InsertResultInEvent(this._deltakere, ev);
            }
            else
            {
                string stages = valid.Aggregate("", (current, stage) => current + stage + " - ");
                var message = "Inneholder flere events.. " + stages;
                MessageBox.Show(message, @"Feil i fil", MessageBoxButtons.OK);
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
    }
}
