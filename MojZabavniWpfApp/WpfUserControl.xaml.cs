using MojZabavniDal.Model;
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
    /// Interaction logic for WpfUserControl.xaml
    /// </summary>
    public partial class WpfUserControl : UserControl
    {
        private Player player;
        private Match match;

        public WpfUserControl(Player p, Match m)
        {
            InitializeComponent();
            this.MouseDown += OnClick;

            this.player = p;
            this.match = m;

            this.lblPlayerName.Text = $"{p.Name.Split()[p.Name.Split().Length - 1]} ({p.ShirtNumber})";


        }

        private void OnClick(Object sender, MouseEventArgs ev)
        {
            new PlayerInformation(this.player, this.match).ShowDialog();


        }
    }
}
