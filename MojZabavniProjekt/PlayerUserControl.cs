using MojZabavniDal.Model;
using MojZabavniDal.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MojZabavniProjekt
{
    public partial class PlayerUserControl : UserControl
    {


        public PictureBox setImg
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }
        public PictureBox setImg2
        {
            get { return pictureBox2; }
            set { pictureBox2 = value; }
        }

        public PlayerUserControl(Player player)
        {
            InitializeComponent();

            label1.Text = player.Name;
            label2.Text = player.ShirtNumber.ToString();
            label3.Text = player.Position.ToString();
            if (player.Captain)
            {
                label4.Text = "Captain";
            }
            else
            {
                label4.Text = "";
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Hide();
            if (File.Exists("images/" + player.Name.ToLower() + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile("images/" + player.Name.ToLower() + ".jpg");
            }
        }

        public string getName()
        {
            return label1.Text;
        }
        public string getShirtNumber()
        {
            return label2.Text;
        }
        public string getPosition()
        {
            return label3.Text;
        }
        public string getCaptain()
        {
            return label4.Text;
        }

        private void PlayerUserControl_DoubleClick(object sender, EventArgs e)
        {

        }


        private void PlayerUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG Files|*.jpg;*.jpeg|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = new System.Drawing.Bitmap(selectedFilePath);
                    string imageName = Path.GetFileNameWithoutExtension(selectedFilePath);

                    string desiredName = label1.Text;

                    if (!string.IsNullOrEmpty(desiredName))
                    {
                        PreferencesRepo.WriteImageAssetNameToFile(desiredName);

                        string lowercaseName = desiredName.ToLower();

                        string destinationPath = Path.Combine("images", $"{lowercaseName}.jpg");

                        File.Copy(selectedFilePath, destinationPath, true);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid name in Label1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading or copying image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PlayerUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
