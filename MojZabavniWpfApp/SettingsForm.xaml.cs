using MojZabavniDal.Interface;
using MojZabavniDal.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MojZabavniWpfApp
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window
    {

        string pathToResource = "../../../../MojZabavniProjekt/bin/Debug/net6.0-windows/";
        ISettingsFileRepo settingsFileRepo;

        public SettingsForm()
        {


            settingsFileRepo = new SettingRepo();
            InitializeComponent();
            cbDataSource.Items.Add("Api");
            cbDataSource.Items.Add("File");
            cbLanguage.Items.Add("EN");
            cbLanguage.Items.Add("HR");
            cbGender.Items.Add("Men");
            cbGender.Items.Add("Women");
            cbResolution.Items.Add("800x568");
            cbResolution.Items.Add("1024x768");
            cbResolution.Items.Add("1280x720");

            FillData();
        }

        private void FillData()
        {
            List<string> content = new List<string>();

            File.ReadAllLines(pathToResource + "Settings.csv").ToList().ForEach(x => content.Add(x));


            cbDataSource.SelectedItem = (content[0] == "Api") ? "Api" : "File";
            cbGender.SelectedItem = (content[1] == "Men") ? "Men" : "Women";
            cbLanguage.SelectedItem = (content[2] == "HR") ? "HR" : "EN";




        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            settingsFileRepo.WriteSettingsFile(new List<string>() { cbDataSource.SelectedItem.ToString(), cbGender.SelectedItem.ToString(), cbLanguage.SelectedItem.ToString() });  
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreferencesRepo.WriteResolutionToFile(cbResolution.SelectedItem.ToString());
        }
    }
}
