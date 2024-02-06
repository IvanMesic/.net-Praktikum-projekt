using MojZabavniDal.Factory;
using MojZabavniDal.Interface;
using MojZabavniDal.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MojZabavniWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataRepo dataRepo;
        List<Player> leftTeamg;
        List<Player> rightTeamg;

        string pathToResource = "../../../../MojZabavniProjekt/bin/Debug/net6.0-windows/";

        public MainWindow()
        {

            initSettings();
            dataRepo = RepoFactory.GetDataRepo();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            setSize();
            
        }

        private void setSize()
        {
            try
            {
                string resolutionString = PreferencesRepo.ReadResolutionFromFile();

                string[] parts = resolutionString.Split('x');
                if (parts.Length == 2)
                {
                    int width = int.Parse(parts[0]);
                    int height = int.Parse(parts[1]);

                    this.Width = width;
                    this.Height = height;
                }
 
            }
            catch (Exception ex)
            {
            }
        }

        private async void fillLabels()
        {
            var match = await dataRepo.GetMatchForTeams(cbLeftTeam.SelectedItem.ToString(), cbRightTeam.SelectedItem.ToString());
            lbLeftTeam.Content = match.AwayTeam.Goals;
            lbRightTeam.Content = match.HomeTeam.Goals;
        }

        private void initSettings()
        {
            SettingsForm settingsForm =  new SettingsForm() ;
            settingsForm.ShowDialog();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var teams = await dataRepo.GetTeamsAsync();
            teams.ToList().ForEach(x => cbLeftTeam.Items.Add(x.FifaCode));
            
        }

        private async void FillMap()
        {
            ClearAllStackPanels();
            var match = await dataRepo.GetMatchForTeams(cbLeftTeam.SelectedItem.ToString(), cbRightTeam.SelectedItem.ToString());
            foreach (var item in leftTeamg )
            {
                WpfUserControl wpfUserControl = new WpfUserControl(item, match);
                switch (item.Position)
                {
                    case Position.Defender:
                        this.stackPanelLeftDefenders.Children.Add(wpfUserControl);
                        break;
                    case Position.Forward:
                        this.stackPanelLeftAttack.Children.Add(wpfUserControl);
                        break;
                    case Position.Goalie:
                        this.stackPanelLeftGoalie.Children.Add(wpfUserControl);
                        break;
                    case Position.Midfield:
                        this.stackPanelLeftMidfield.Children.Add(wpfUserControl);
                        break;
                    default:
                        break;
                }
            }
            foreach (var item in rightTeamg)
            {
                WpfUserControl wpfUserControl = new WpfUserControl(item, match);
                switch (item.Position)
                {
                    case Position.Defender:
                        this.stackPanelRightDefenders.Children.Add(wpfUserControl);
                        break;
                    case Position.Forward:
                        this.stackPanelRightAttack.Children.Add(wpfUserControl);
                        break;
                    case Position.Goalie:
                        stackPanelRightGoalie.Children.Add(wpfUserControl);
                        break;
                    case Position.Midfield:
                        this.stackPanelRightMidfield.Children.Add(wpfUserControl);
                        break;
                    default:
                        break;
                }
            }
        }

        private async void cbLeftTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbRightTeam.Items.Clear();
            ClearAllStackPanels();

            List<Team> teams = (await dataRepo.GetOpponentTeams(cbLeftTeam.SelectedItem.ToString())).ToList();
            
            teams.ForEach(x => cbRightTeam.Items.Add(x.FifaCode));
        }

        private void cbRightTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRightTeam.SelectedItem == null || cbRightTeam.SelectedItem == "" )
            {
                return;
            }

            List<Player> leftTeam = dataRepo.GetStartingElevenAsync(cbLeftTeam.SelectedItem.ToString()).Result.ToList();

            List<Player> rightTeam = dataRepo.GetStartingElevenAsync(cbRightTeam.SelectedItem.ToString()).Result.ToList();

            leftTeamg = leftTeam;
            rightTeamg = rightTeam;
            FillMap();
            fillLabels();

        }

        private void ClearAllStackPanels()
        {
            stackPanelLeftGoalie.Children.Clear();
            stackPanelLeftDefenders.Children.Clear();
            stackPanelLeftMidfield.Children.Clear();
            stackPanelLeftAttack.Children.Clear();
            stackPanelRightAttack.Children.Clear();
            stackPanelRightMidfield.Children.Clear();
            stackPanelRightDefenders.Children.Clear();
            stackPanelRightGoalie.Children.Clear();
        }

        private void ClearRightStackPanels()
        {
            stackPanelRightAttack.Children.Clear();
            stackPanelRightMidfield.Children.Clear();
            stackPanelRightDefenders.Children.Clear();
            stackPanelRightGoalie.Children.Clear();
        }

        private void detailsLeft_Click(object sender, RoutedEventArgs e)
        {
            string code = cbLeftTeam.SelectedItem.ToString();
            Window1 window1 = new Window1(code);
            window1.ShowDialog();
        }

        private void detailsRight_Click(object sender, RoutedEventArgs e)
        {
            string code = cbRightTeam.SelectedItem.ToString();
            Window1 window1 = new Window1(code);
            window1.ShowDialog();
        }
    }
}
