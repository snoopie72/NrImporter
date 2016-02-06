using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
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
            var sqlDirectService = new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            var datalayerService = new DatalayerService(sqlDirectService);
            var eventService = new EventService(datalayerService);
            _handler = new EventResultHandler(new UserService(datalayerService),
                    eventService, new FilterService(datalayerService));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = ".csv"; //virker ikke
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
                var deltaker = new ExcelLoader().LoadRaceResult(stream);
                deltaker = _handler.FilterDeltakere(deltaker);
                Form form2 = new Form2(deltaker, _handler);
                form2.ShowDialog(this);
            }
            
            //this.Close();
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
