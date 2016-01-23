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
            var x = new MockedEventService();
            textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
        }
    }
}
