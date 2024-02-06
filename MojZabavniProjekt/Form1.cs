using MojZabavniDal.Factory;
using MojZabavniDal.Interface;
using MojZabavniDal.Model;
using MojZabavniDal.Repo;
using System.Windows.Forms;

namespace MojZabavniProjekt
{
    public partial class Form1 : Form
    {
        private IDataRepo dataRepo;

        public Form1()
        {
            InitializeComponent();

            flowLayoutPanel1.DragEnter += flowLayoutPanel1_DragEnter;
            flowLayoutPanel2.DragEnter += flowLayoutPanel2_DragEnter;

            flowLayoutPanel1.DragDrop += flowLayoutPanel1_DragDrop;
            flowLayoutPanel2.DragDrop += flowLayoutPanel2_DragDrop;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            var players = dataRepo.GetPlayersAsync(comboBox1.SelectedItem.ToString());

            players.Result.ToList().ForEach(x => flowLayoutPanel1.Controls.Add(new PlayerUserControl(x)));

        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();


            dataRepo = RepoFactory.GetDataRepo();
            var teams = await dataRepo.GetTeamsAsync();
            teams.ToList().ForEach(x => comboBox1.Items.Add(x.FifaCode));
        }

        private async void button1_Click(object sender, EventArgs e)
        {


        }

        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerUserControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerUserControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            Control draggedControl = e.Data.GetData(typeof(PlayerUserControl)) as Control;
            PlayerUserControl playerUserControl = (PlayerUserControl)e.Data.GetData(typeof(PlayerUserControl));

            if (draggedControl != null)
            {
                draggedControl.Parent.Controls.Remove(draggedControl);

                FlowLayoutPanel destinationPanel = sender as FlowLayoutPanel;
                destinationPanel.Controls.Add(draggedControl);
                playerUserControl.pictureBox2.Hide();
            }
        }

        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            Control draggedControl = e.Data.GetData(typeof(PlayerUserControl)) as Control;
            PlayerUserControl playerUserControl = (PlayerUserControl)e.Data.GetData(typeof(PlayerUserControl));

            if (flowLayoutPanel2.Controls.Count >= 3)
            {
                ShowMaxSizePopup();
                return;
            }

            if (draggedControl != null)
            {
                draggedControl.Parent.Controls.Remove(draggedControl);

                FlowLayoutPanel destinationPanel = sender as FlowLayoutPanel;
                destinationPanel.Controls.Add(draggedControl);
                playerUserControl.pictureBox2.Show();
            }
        }

        private void ShowMaxSizePopup()
        {
            MessageBox.Show("Maximum size reached! You can only add up to 3 items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>();

            foreach (var item in flowLayoutPanel2.Controls)
            {
                if (item is PlayerUserControl)
                {
                    PlayerUserControl playerUserControl = item as PlayerUserControl;

                    players.Add(new Player()
                    {
                        Name = playerUserControl.getName(),
                        ShirtNumber = int.Parse(playerUserControl.getShirtNumber()),
                        Position = ParsePosition(playerUserControl.getPosition()),
                        Captain = playerUserControl.getCaptain() == "Captain"
                    });

                }
            }
            PreferencesRepo.WriteTeamToFile(players);
        }

        private Position ParsePosition(string positionString)
        {
            return (Position)Enum.Parse(typeof(Position), positionString);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            List<Team>? teams = await dataRepo.GetTeamsAsync() as List<Team>;
            string country = teams.Find(x => x.FifaCode == comboBox1.Text).Country;

            List<string> strings = new List<string>() { country, comboBox1.Text };

            PreferencesRepo.WriteTeamCodeToFile(strings);
        }

        private void rangListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RangForm settingsForm = new RangForm();

        }
    }
}