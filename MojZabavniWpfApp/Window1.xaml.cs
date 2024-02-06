using MojZabavniDal.Factory;
using MojZabavniDal.Interface;
using MojZabavniDal.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        IDataRepo dataRepo;
        string code = "";
        public  Window1(string fifaCode)
        {
            InitializeComponent();
            code = fifaCode;
            dataRepo = RepoFactory.GetDataRepo();
        }




        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var team = await dataRepo.GetTeamResults(code);

            Country.Content = team.Country;
            Code.Content = team.FifaCode;
            Goals.Content = team.GoalsFor;
            Wins.Content = team.Wins;
            Draws.Content = team.Draws;
            Losses.Content = team.Losses;
            GamesPlayed.Content = team.GamesPlayed;
            Taken.Content = team.GoalsAgainst;
            Diff.Content = team.GoalDifferential;


        }
    }
}
