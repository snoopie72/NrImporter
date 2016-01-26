using Northernrunners.ImportLibrary.Service.Mocked;
using Northernrunners.ImportLibrary.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NR_Resultat_Import
{
    public partial class Form2 : Form
    {
        private List<Deltaker> deltakere;

        public Form2(List<Deltaker> deltakere)
        {
            InitializeComponent();
            this.deltakere = deltakere;
            var bindingList = new BindingList<Deltaker>(deltakere);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            if (deltakere.First() != null)
            {
                var x = new MockedEventService();
                //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
                List<Event> eventer = x.GetEvents("adsf", DateTime.Now.Year).ToList();
                if (eventer.Count == 1)
                {
                    label2.Text = String.Format("ID={0}", eventer.First().Id);
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
    }
}
