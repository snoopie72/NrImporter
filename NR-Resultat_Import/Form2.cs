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
        }
    }
}
