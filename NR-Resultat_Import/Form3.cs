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
//using System.Reflection;

namespace NR_Resultat_Import
{
    public partial class Form3 : Form
    {
        //private Assembly _assembly;

        public Form3()
        {
            InitializeComponent();
            //_assembly = Assembly.GetExecutingAssembly();
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            var filter = @"C:\Users\KaiHugo\Documents\Kode\NrImporter\NRImporter.Tests\Resources\Filter.csv";

            //using (var stream = _assembly.GetManifestResourceStream(filter))
            using (var stream = new FileStream(filter, FileMode.Open))
            {
                int i = 1;
                using (var streamReader = new StreamReader(stream, encoding))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Replace("\"", "");
                        Controls["label" + i].Text = line.Split(';')[0];
                        Controls["textBox" + i].Text = line.Split(';')[1];
                        i++;
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (label1.Text.Equals("Contains"))
                label1.Text = "Equals";
            else
                label1.Text = "Contains";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (label2.Text.Equals("Contains"))
                label2.Text = "Equals";
            else
                label2.Text = "Contains";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label3.Text.Equals("Contains"))
                label3.Text = "Equals";
            else
                label3.Text = "Contains";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Text.Equals("Contains"))
                label4.Text = "Equals";
            else
                label4.Text = "Contains";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (label5.Text.Equals("Contains"))
                label5.Text = "Equals";
            else
                label5.Text = "Contains";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (label6.Text.Equals("Contains"))
                label6.Text = "Equals";
            else
                label6.Text = "Contains";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (label7.Text.Equals("Contains"))
                label7.Text = "Equals";
            else
                label7.Text = "Contains";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (label8.Text.Equals("Contains"))
                label8.Text = "Equals";
            else
                label8.Text = "Contains";
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            var filter = @"C:\Users\KaiHugo\Documents\Kode\NrImporter\NRImporter.Tests\Resources\Filter.csv";

            using (var stream = new FileStream(filter, FileMode.Create))
            {
                using (var streamWriter = new StreamWriter(stream, encoding))
                {
                    for (int i = 1; i < 9; i++)
                    {
                        string value = Controls["textBox" + i].Text;
                        if (value != null && value.Length > 0)
                        {
                            string line = Controls["label" + i].Text + ";" + value;
                            streamWriter.WriteLine(line);
                        }
                    }
                    streamWriter.Flush();
                }
            }
        }
    }
}
