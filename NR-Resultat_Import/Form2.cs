using Northernrunners.ImportLibrary.Service.Mocked;
using Northernrunners.ImportLibrary.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Utils;

namespace NR_Resultat_Import
{
    public partial class Form2 : Form
    {
        private List<Deltaker> deltakere;
        private IUserHandler _userHandler;

        public Form2(List<Deltaker> deltakere)
        {
            _userHandler = new UserHandler(new UserService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            InitializeComponent();
            this.deltakere = deltakere;
            var bindingList = new BindingList<Deltaker>(deltakere);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            if (deltakere.First() != null)
            {
                //var x = new MockedEventService();
                var eventService = new EventService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString));
                //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
                //List<Event> eventer = eventService.GetEvents("adsf", DateTime.Now.Year).ToList();
                var eventer = eventService.GetEvents(new DateTime(2015, 1, 1), DateTime.Now);
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
            var x = dataGridView1.Rows;
            var collection = new List<User>();
            foreach (DataGridViewRow row in x)
            {
                var username = Convert.ToString(row.Cells[0].Value);
                if (String.IsNullOrEmpty(username))
                {
                    break;
                }
                var gender = Convert.ToString(row.Cells[1].Value);

                var user = new User
                {
                    Name = username,
                    Male = gender.ToLower().Equals("m"),
                    Email = ""
                };
                collection.Add(user);

            }
            Tools.RandomizeDateOfBirth(collection);
            var users = _userHandler.LoadUserDetails(collection);


        }
    }
}
