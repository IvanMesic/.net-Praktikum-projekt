namespace MojZabavniProjekt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            comboBox1 = new ComboBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            menuStrip1 = new MenuStrip();
            rangListToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.Name = "comboBox1";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AllowDrop = true;
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.DragDrop += flowLayoutPanel1_DragDrop;
            flowLayoutPanel1.DragEnter += flowLayoutPanel1_DragEnter;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AllowDrop = true;
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.DragDrop += flowLayoutPanel2_DragDrop;
            flowLayoutPanel2.DragEnter += flowLayoutPanel2_DragEnter;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { rangListToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // rangListToolStripMenuItem
            // 
            rangListToolStripMenuItem.Name = "rangListToolStripMenuItem";
            resources.ApplyResources(rangListToolStripMenuItem, "rangListToolStripMenuItem");
            rangListToolStripMenuItem.Click += rangListToolStripMenuItem_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrip1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(comboBox1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Load += Form1_LoadAsync;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button button1;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem rangListToolStripMenuItem;
    }
}