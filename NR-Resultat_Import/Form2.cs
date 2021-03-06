﻿using Northernrunners.ImportLibrary.Poco;
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
        private List<string> _stages;
        public Form2(ICollection<UserEventInfo> deltakere, EventResultHandler handler, DateTime evtDate)
        {
            _eventResultHandler = handler;
            
            InitializeComponent();
            this._deltakere = deltakere;
            var bindingList = new BindingList<UserEventInfo>(deltakere.ToList());
            var source = new BindingSource(bindingList,null);
            dataGridView1.DataSource = source;
            dataGridView1.ReadOnly = false;
            this._stages = getStages();
            if (deltakere.First() != null)
            {
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MaxValue;
                if (evtDate > DateTime.MinValue)
                {
                    startDate = evtDate;
                    endDate = evtDate.AddDays(1).AddSeconds(-1);
                }
                var eventer = _eventResultHandler.GetEvents(startDate, endDate);
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
                if (this._stages.Count>1)
                {
                    ICollection<UserEventInfo> temp = new List<UserEventInfo>();
                    string stage = comboBoxStages.SelectedItem.ToString();
                    foreach (var deltaker in this._deltakere.Where(deltaker => deltaker.Stage.Equals(stage)))
                    {
                        temp.Add(deltaker);
                    }
                    this._deltakere = temp;
                }
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                Event ev = (Event)listBox1.SelectedItem;
                this.backgroundWorker1.RunWorkerAsync(ev);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSubmitResults.Enabled = true;
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

        private List<string> getStages()
        {
            SortedSet<string> stages = new SortedSet<string>();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                try
                {
                    string stage = ((UserEventInfo)r.DataBoundItem).Stage;
                    stages.Add(stage);
                }
                catch (NullReferenceException) { }
            }
            if (stages.Count>1)
            {
                comboBoxStages.Items.Clear();
                foreach (string stage in stages)
                {
                    comboBoxStages.Items.Add(stage);
                }
                comboBoxStages.Visible = true;
                LoadUsers.Enabled = false;
                btnSubmitResults.Enabled = false;
            }
            return stages.ToList<string>();
        }

        private void comboBoxStages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStages.SelectedItem.ToString() != "Velg stage")
            {
                LoadUsers.Enabled = true;
                btnSubmitResults.Enabled = true;
            }
            else
            {
                LoadUsers.Enabled = false;
                btnSubmitResults.Enabled = false;
            }
        }
    }
}
