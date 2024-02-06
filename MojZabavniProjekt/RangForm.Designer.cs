namespace MojZabavniProjekt
{
    partial class RangForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            Location = new DataGridViewTextBoxColumn();
            Attendance = new DataGridViewTextBoxColumn();
            HomeTeam = new DataGridViewTextBoxColumn();
            AwayTeam = new DataGridViewTextBoxColumn();
            btnPreview = new Button();
            btnSave = new Button();
            Picture = new DataGridViewImageColumn();
            Name = new DataGridViewTextBoxColumn();
            Goals = new DataGridViewTextBoxColumn();
            YellowCards = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Picture, Name, Goals, YellowCards });
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(553, 426);
            dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Location, Attendance, HomeTeam, AwayTeam });
            dataGridView2.Location = new Point(587, 12);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.RowTemplate.Height = 29;
            dataGridView2.Size = new Size(553, 426);
            dataGridView2.TabIndex = 1;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // Location
            // 
            Location.HeaderText = "Location";
            Location.MinimumWidth = 6;
            Location.Name = "Location";
            Location.Width = 125;
            // 
            // Attendance
            // 
            Attendance.HeaderText = "Attendance";
            Attendance.MinimumWidth = 6;
            Attendance.Name = "Attendance";
            Attendance.Width = 125;
            // 
            // HomeTeam
            // 
            HomeTeam.HeaderText = "HomeTeam";
            HomeTeam.MinimumWidth = 6;
            HomeTeam.Name = "HomeTeam";
            HomeTeam.Width = 125;
            // 
            // AwayTeam
            // 
            AwayTeam.HeaderText = "AwayTeam";
            AwayTeam.MinimumWidth = 6;
            AwayTeam.Name = "AwayTeam";
            AwayTeam.Width = 125;
            // 
            // btnPreview
            // 
            btnPreview.Location = new Point(206, 491);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(138, 67);
            btnPreview.TabIndex = 2;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(768, 491);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(158, 67);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save Pdf";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Picture
            // 
            Picture.HeaderText = "Picture";
            Picture.MinimumWidth = 6;
            Picture.Name = "Picture";
            Picture.Width = 125;
            // 
            // Name
            // 
            Name.HeaderText = "Name";
            Name.MinimumWidth = 6;
            Name.Name = "Name";
            Name.Width = 125;
            // 
            // Goals
            // 
            Goals.HeaderText = "Goals";
            Goals.MinimumWidth = 6;
            Goals.Name = "Goals";
            Goals.Width = 125;
            // 
            // YellowCards
            // 
            YellowCards.HeaderText = "Yellows";
            YellowCards.MinimumWidth = 6;
            YellowCards.Name = "YellowCards";
            YellowCards.Width = 125;
            // 
            // RangForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1269, 624);
            Controls.Add(btnSave);
            Controls.Add(btnPreview);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Text = "RangForm";
            Load += RangForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn Location;
        private DataGridViewTextBoxColumn Attendance;
        private DataGridViewTextBoxColumn HomeTeam;
        private DataGridViewTextBoxColumn AwayTeam;
        private Button btnPreview;
        private Button btnSave;
        private DataGridViewImageColumn Picture;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Goals;
        private DataGridViewTextBoxColumn YellowCards;
    }
}