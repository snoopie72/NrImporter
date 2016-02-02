using Northernrunners.ImportLibrary.Service.Mocked;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Northernrunners.ImportLibrary.Excel;

namespace NR_Resultat_Import
{
    public partial class Form1 : Form
    {
        public Form1()
        {
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
            var deltaker = ExcelLoader.LoadRaceResult(textBox1.Text, "Northern Runners");

            Form form2 = new Form2(deltaker);
            form2.ShowDialog(this);
            //this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FileInfo f = new FileInfo(textBox1.Text);
                if (f.Exists)
                    button2.Enabled = true;
                else
                    button2.Enabled = false;
            }
            catch (Exception) {}
        }
    }
}
