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
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service;

//using System.Reflection;

namespace NR_Resultat_Import
{
    public partial class Form3 : Form
    {
        private readonly EventResultHandler _handler;
        //private Assembly _assembly;

        public Form3(EventResultHandler handler)
        {
            _handler = handler;
            InitializeComponent();
            
            
            var filters = _handler.GetFilters();
            var filterList = filters.ToList();
            for (var i = 0; i < filters.Count; i++)
            {
                var j = i + 1;
                Controls["label" + j].Text = filterList[i].Type.ToString();
                Controls["textBox" + j].Text = filterList[i].Value;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = label2.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = label3.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text = label4.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label5.Text = label5.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.Text = label6.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            label7.Text = label7.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Text = label8.Text.Equals(FilterType.Contains.ToString()) ? FilterType.Equals.ToString() : FilterType.Contains.ToString();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            var filters = new List<Filter>();
            for (var i = 1; i < 9; i++)
            {
                var value = Controls["textBox" + i].Text;
                if (string.IsNullOrEmpty(value)) continue;
                var key = Controls["label" + i].Text;
                var filter = new Filter
                {
                    Type =
                        key.Equals(FilterType.Equals.ToString()) ? FilterType.Equals : FilterType.Contains,
                    Value = value
                };
                filters.Add(filter);
            }
            _handler.SaveFilters(filters);
        }
    }
}
