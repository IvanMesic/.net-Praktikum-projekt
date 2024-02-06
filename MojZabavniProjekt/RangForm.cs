using MojZabavniDal.Factory;
using MojZabavniDal.Interface;
using MojZabavniDal.Model;
using MojZabavniDal.Repo;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MojZabavniProjekt
{
    public partial class RangForm : Form
    {

        private delegate void DelegateForMatches(string location, int attendance, string homeTeam, string awayTeam);
        private delegate void DelegateForPlayers(Player player, int goals, int yellow_cards);


        private IDataRepo dataRepo;
        public RangForm()
        {
            this.Show();
            InitializeComponent();
            dataRepo = RepoFactory.GetDataRepo();
            LoadData();
        }

        private void FillMathces(string location, int attendance, string homeTeam, string awayTeam)
        {
            dataGridView2.Rows.Add(location, attendance, homeTeam, awayTeam);
        }
        private void FillPlayers(Player player, int goals, int yellow_cards)
        {
            var playerControl = new PlayerUserControl(player);
            Image imagetoSet = playerControl.pictureBox1.Image;

            //add image from playerControl.picturebox1 to datagridview1 first row
            Bitmap playerimg = new Bitmap(playerControl.pictureBox1.Image);
            dataGridView1.Rows.Add(playerimg, player.Name, goals, yellow_cards);

            if (dataGridView1.Rows.Count == 23)
            {
                btnPreview.Visible = true;
                btnSave.Visible = true;
            }
        }

        private async void LoadData()
        {

            string fifaCode = PreferencesRepo.ReadTeamCodeAndCountryFromFile()[1];
            string country = PreferencesRepo.ReadTeamCodeAndCountryFromFile()[0];
            List<Player>? players = await dataRepo.GetPlayersAsync(fifaCode) as List<Player>;
            int yellow = 0;
            int goals = 0;
            bool firstTime = true;

            var matches = await dataRepo.GetSpecificMatchesAsync(country) as List<MojZabavniDal.Model.Match>;

            foreach (Player player in players)
            {
                foreach (MojZabavniDal.Model.Match match in matches)
                {
                    List<TeamEvent> teamEvents;

                    if (match.AwayTeamStatistics.Country == PreferencesRepo.ReadTeamCodeAndCountryFromFile()[1])
                    {
                        teamEvents = match.AwayTeamEvents.ToList();
                    }
                    else
                    {
                        teamEvents = match.AwayTeamEvents.ToList();
                    }


                    foreach (TeamEvent teamEvent in teamEvents)
                    {
                        if (teamEvent.Player.ToLower() == player.Name.ToLower())
                        {
                            if (teamEvent.TypeOfEvent == EventType.Goal)
                            {
                                goals++;
                            }

                            else if (teamEvent.TypeOfEvent == EventType.YellowCard)
                            {
                                yellow++;
                            }
                        }
                    }


                    if (firstTime)
                    {
                        dataGridView2.Invoke(new DelegateForMatches(FillMathces),
                            match.Location,
                            Convert.ToInt32(match.Attendance),
                            match.HomeTeamStatistics.Country,
                            match.AwayTeamStatistics.Country);

                    }
                }
                firstTime = false;
                dataGridView1.Invoke(new DelegateForPlayers(FillPlayers), player, goals, yellow);
                yellow = 0;
                goals = 0;

            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RangForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Show the SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Rankings Data";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    GlobalFontSettings.FontResolver = new CustomFontResolver();

                    using (PdfDocument pdfDocument = new PdfDocument())
                    {
                        PdfPage page = pdfDocument.AddPage();

                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        XFont font = new XFont("Open Sans", 10);

                        AddDataGridViewToPdf(dataGridView1, gfx, font);

                        page = pdfDocument.AddPage();
                        gfx = XGraphics.FromPdfPage(page);

                        AddDataGridViewToPdf(dataGridView2, gfx, font);


                        pdfDocument.Save(filePath);
                    }

                    MessageBox.Show("Data saved successfully as PDF!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data as PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddDataGridViewToPdf(DataGridView dataGridView, XGraphics gfx, XFont font)
        {
            double x = 10, y = 10;
            double columnWidth = 80;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                gfx.DrawString(column.HeaderText, font, XBrushes.Black, x, y);
                x += columnWidth + 10;
            }


            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                x = 10;
                y += 20;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    gfx.DrawString(cell.Value?.ToString() ?? "", font, XBrushes.Black, x, y);
                    x += columnWidth + 10;
                }
            }
        }


        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintPage;

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;

                // Show the print preview dialog
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error previewing PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float x = 10, y = 10;
            float columnWidth = 80;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                g.DrawString(column.HeaderText, Font, Brushes.Black, x, y);
                x += columnWidth + 10;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                x = 10;
                y += 20;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    g.DrawString(cell.Value?.ToString() ?? "", Font, Brushes.Black, x, y);
                    x += columnWidth + 10;
                }
            }

            x = 10;
            y += 40;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                g.DrawString(column.HeaderText, Font, Brushes.Black, x, y);
                x += columnWidth + 10;
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                x = 10;
                y += 20;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    g.DrawString(cell.Value?.ToString() ?? "", Font, Brushes.Black, x, y);
                    x += columnWidth + 10;
                }
            }
        }
    }
}