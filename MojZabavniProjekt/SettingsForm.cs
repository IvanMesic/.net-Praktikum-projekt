using MojZabavniDal.Interface;
using MojZabavniDal.Repo;
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

namespace MojZabavniProjekt
{
    public partial class SettingsForm : Form
    {
        ISettingsFileRepo SettingsFileRepo;

        public SettingsForm()
        {
            InitializeComponent();
            SettingsFileRepo = new SettingRepo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(comboBox1.Text);
            list.Add(comboBox2.Text);
            list.Add(comboBox3.Text);

            SettingsFileRepo.WriteSettingsFile(list);

        }
    }
}
