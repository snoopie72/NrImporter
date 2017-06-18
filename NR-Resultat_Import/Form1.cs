using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Northernrunners.ImportLibrary.Excel;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Datalayer;

namespace NR_Resultat_Import
{
    public partial class Form1 : Form
    {
        private readonly EventResultHandler _handler;
        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            var sqlDirectService = new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            var datalayerService = new DatalayerService(sqlDirectService);
            var eventService = new EventService(datalayerService);
            _handler = new EventResultHandler(new UserService(datalayerService),
                    eventService, new FilterService(datalayerService));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "*.csv"; //virker ikke
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var stream = new FileStream(textBox1.Text, FileMode.Open))
            {
                DateTime evtDate = getEventdate(stream.Name);
                var deltaker = new ExcelLoader().LoadRaceResult(stream);
                deltaker = _handler.FilterDeltakere(deltaker);
                Form form2 = new Form2(deltaker, _handler, evtDate);
                form2.ShowDialog(this);
            }
            
            //this.Close();
        }

        private DateTime getEventdate(string filename)
        {
            DateTime evtDate = DateTime.MinValue;
            try
            {
                filename = filename.Substring(filename.LastIndexOf(@"\") + 1);
                int y = Convert.ToInt16(filename.Substring(0, 4));
                int m = Convert.ToInt16(filename.Substring(5, 2));
                int d = Convert.ToInt16(filename.Substring(8, 2));
                evtDate = new DateTime(y, m, d);
            }
            catch {}
            return evtDate;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var f = new FileInfo(textBox1.Text);
                btnImport.Enabled = f.Exists;
            }
            catch (Exception) {}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f3 = new Form3(_handler);
            f3.ShowDialog(this);
        }
    }
}
