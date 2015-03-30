namespace ImageRenamer
{
    partial class MainForm
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
            this.FlowImagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.FlowButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PrefixBox = new System.Windows.Forms.TextBox();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.RenameButton = new System.Windows.Forms.Button();
            this.ClearSelectionButton = new System.Windows.Forms.Button();
            this.FlowButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlowImagePanel
            // 
            this.FlowImagePanel.AutoScroll = true;
            this.FlowImagePanel.BackColor = System.Drawing.Color.Transparent;
            this.FlowImagePanel.Location = new System.Drawing.Point(0, 60);
            this.FlowImagePanel.Margin = new System.Windows.Forms.Padding(20);
            this.FlowImagePanel.Name = "FlowImagePanel";
            this.FlowImagePanel.Padding = new System.Windows.Forms.Padding(15);
            this.FlowImagePanel.Size = new System.Drawing.Size(13, 10);
            this.FlowImagePanel.TabIndex = 4;
            // 
            // FlowButtonsPanel
            // 
            this.FlowButtonsPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.FlowButtonsPanel.BackgroundImage = global::ImageRenamer.Properties.Resources.nav_panel_background1;
            this.FlowButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlowButtonsPanel.Controls.Add(this.PrefixBox);
            this.FlowButtonsPanel.Controls.Add(this.OpenFolderButton);
            this.FlowButtonsPanel.Controls.Add(this.RenameButton);
            this.FlowButtonsPanel.Controls.Add(this.ClearSelectionButton);
            this.FlowButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FlowButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FlowButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.FlowButtonsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FlowButtonsPanel.Name = "FlowButtonsPanel";
            this.FlowButtonsPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FlowButtonsPanel.Size = new System.Drawing.Size(814, 60);
            this.FlowButtonsPanel.TabIndex = 7;
            // 
            // PrefixBox
            // 
            this.PrefixBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PrefixBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.PrefixBox.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrefixBox.ForeColor = System.Drawing.Color.LightSlateGray;
            this.PrefixBox.Location = new System.Drawing.Point(20, 15);
            this.PrefixBox.Margin = new System.Windows.Forms.Padding(15, 15, 20, 15);
            this.PrefixBox.MaxLength = 30;
            this.PrefixBox.Name = "PrefixBox";
            this.PrefixBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PrefixBox.Size = new System.Drawing.Size(200, 27);
            this.PrefixBox.TabIndex = 1;
            this.PrefixBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PrefixBox.Leave += new System.EventHandler(this.PrefixBox_Leave);
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Image = global::ImageRenamer.Properties.Resources.open_icon;
            this.OpenFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OpenFolderButton.Location = new System.Drawing.Point(238, 3);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(140, 50);
            this.OpenFolderButton.TabIndex = 7;
            this.OpenFolderButton.Text = "Отвори папка";
            this.OpenFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OpenFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // RenameButton
            // 
            this.RenameButton.Image = global::ImageRenamer.Properties.Resources.save_icon;
            this.RenameButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RenameButton.Location = new System.Drawing.Point(384, 3);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(140, 50);
            this.RenameButton.TabIndex = 6;
            this.RenameButton.Text = "Преименувай";
            this.RenameButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RenameButton.UseVisualStyleBackColor = true;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // ClearSelectionButton
            // 
            this.ClearSelectionButton.Image = global::ImageRenamer.Properties.Resources.clear_selection;
            this.ClearSelectionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearSelectionButton.Location = new System.Drawing.Point(530, 3);
            this.ClearSelectionButton.Name = "ClearSelectionButton";
            this.ClearSelectionButton.Size = new System.Drawing.Size(140, 50);
            this.ClearSelectionButton.TabIndex = 8;
            this.ClearSelectionButton.Text = "Изчисти селекция";
            this.ClearSelectionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearSelectionButton.UseVisualStyleBackColor = true;
            this.ClearSelectionButton.Click += new System.EventHandler(this.ClearSelection_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(814, 598);
            this.Controls.Add(this.FlowButtonsPanel);
            this.Controls.Add(this.FlowImagePanel);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vivacom.RDNS ImageRenamer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.FlowButtonsPanel.ResumeLayout(false);
            this.FlowButtonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox PrefixBox;
        private System.Windows.Forms.FlowLayoutPanel FlowImagePanel;
        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.FlowLayoutPanel FlowButtonsPanel;
        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.Button ClearSelectionButton;

    }
}

