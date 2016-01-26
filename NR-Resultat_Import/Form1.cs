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
            List<Deltaker> deltakere = new List<Deltaker>();
            //var x = new MockedEventService();
            //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
            Encoding enc = Encoding.GetEncoding("ISO-8859-1");
            string[] lines = File.ReadAllLines(textBox1.Text, enc);
            for (int i=0; i<lines.Length; i++)
            {
                string[] data = lines[i].Split(";".ToCharArray(), StringSplitOptions.None);
                if (data[4].Contains("Northern Runners"))
                {
                    Deltaker d = new Deltaker
                    {
                        Name = String.Format("{0} {1}", data[1], data[2]),
                        Gender = data[3],
                        Time = data[8],
                        Stage = data[5],
                        Place = data[11]
                    };
                    deltakere.Add(d);
                }
            }
            Form form2 = new Form2(deltakere);
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
