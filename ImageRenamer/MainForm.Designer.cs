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
            this.RenameButton = new System.Windows.Forms.Button();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.PrefixBox = new System.Windows.Forms.TextBox();
            this.FlowImagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BackgroundImageLoader = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // RenameButton
            // 
            this.RenameButton.BackColor = System.Drawing.Color.SkyBlue;
            this.RenameButton.Location = new System.Drawing.Point(620, 12);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(182, 37);
            this.RenameButton.TabIndex = 3;
            this.RenameButton.Text = "Rename";
            this.RenameButton.UseVisualStyleBackColor = false;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.OpenFolderButton.Location = new System.Drawing.Point(432, 12);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(182, 37);
            this.OpenFolderButton.TabIndex = 0;
            this.OpenFolderButton.Text = "Open Folder";
            this.OpenFolderButton.UseVisualStyleBackColor = false;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // PrefixBox
            // 
            this.PrefixBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PrefixBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.PrefixBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrefixBox.Location = new System.Drawing.Point(13, 17);
            this.PrefixBox.Name = "PrefixBox";
            this.PrefixBox.Size = new System.Drawing.Size(413, 24);
            this.PrefixBox.TabIndex = 1;
            // 
            // FlowImagePanel
            // 
            this.FlowImagePanel.AutoScroll = true;
            this.FlowImagePanel.BackColor = System.Drawing.Color.DimGray;
            this.FlowImagePanel.Location = new System.Drawing.Point(3, 55);
            this.FlowImagePanel.Name = "FlowImagePanel";
            this.FlowImagePanel.Size = new System.Drawing.Size(13, 10);
            this.FlowImagePanel.TabIndex = 4;
            // 
            // BackgroundImageLoader
            // 
            this.BackgroundImageLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundImageLoader_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(814, 598);
            this.Controls.Add(this.FlowImagePanel);
            this.Controls.Add(this.PrefixBox);
            this.Controls.Add(this.OpenFolderButton);
            this.Controls.Add(this.RenameButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vivacom.RDNS ImageRenamer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.TextBox PrefixBox;
        private System.Windows.Forms.FlowLayoutPanel FlowImagePanel;
        private System.ComponentModel.BackgroundWorker BackgroundImageLoader;
    }
}

