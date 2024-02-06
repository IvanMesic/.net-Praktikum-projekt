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
    /// Interaction logic for PlayerInformation.xaml
    /// </summary>
    public partial class PlayerInformation : Window
    {
        private int goals;
        private int cards;
        public PlayerInformation(Player p, Match m )
        {
            InitializeComponent();
            FindNeededData(p, m);
            FillData(p);

        }

        private void FillData(Player p)
        {
            lbName.Content = p.Name;
            lbNumber.Content = p.ShirtNumber;
            lbPosition.Content = p.Position;
            lbGoals.Content = goals;
            lbYellowCards.Content = cards;
            if (p.Captain)
            {
                lbIsCaptain.Content = "Captain";
            }
            else
            {
                lbIsCaptain.Content = "";
            }

        }

        private void FindNeededData(Player p,Match m)
        {
            foreach (var item in m.AwayTeamEvents)
            {
                if (item.TypeOfEvent == EventType.Goal && item.Player == p.Name)
                {
                   goals++;
                }
                if (item.TypeOfEvent == EventType.YellowCard && item.Player == p.Name)
                {
                    cards++;
                }
                if (item.TypeOfEvent == EventType.SecondYellowCard && item.Player == p.Name)
                {
                    cards++;
                }
            }
        }
    }
}
