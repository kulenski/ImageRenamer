namespace eu.kulenski.appkitchen.ImageRenamer
{
    partial class ImageHolder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HolderLabel = new System.Windows.Forms.Label();
            this.RackSubButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.RackButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HolderLabel
            // 
            this.HolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HolderLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.HolderLabel.Location = new System.Drawing.Point(3, 264);
            this.HolderLabel.Name = "HolderLabel";
            this.HolderLabel.Size = new System.Drawing.Size(256, 24);
            this.HolderLabel.TabIndex = 0;
            this.HolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RackSubButton
            // 
            this.RackSubButton.Image = global::eu.kulenski.appkitchen.ImageRenamer.Properties.Resources.AddRackSubButtonIcon;
            this.RackSubButton.Location = new System.Drawing.Point(109, 88);
            this.RackSubButton.Name = "RackSubButton";
            this.RackSubButton.Size = new System.Drawing.Size(48, 48);
            this.RackSubButton.TabIndex = 2;
            this.RackSubButton.UseVisualStyleBackColor = true;
            this.RackSubButton.Click += new System.EventHandler(this.RackSubButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.Image = global::eu.kulenski.appkitchen.ImageRenamer.Properties.Resources.AddViewButtonIcon;
            this.ViewButton.Location = new System.Drawing.Point(165, 88);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.Size = new System.Drawing.Size(48, 48);
            this.ViewButton.TabIndex = 2;
            this.ViewButton.UseVisualStyleBackColor = true;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // RackButton
            // 
            this.RackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RackButton.Image = global::eu.kulenski.appkitchen.ImageRenamer.Properties.Resources.AddRackButtonIcon;
            this.RackButton.Location = new System.Drawing.Point(53, 88);
            this.RackButton.Name = "RackButton";
            this.RackButton.Size = new System.Drawing.Size(48, 48);
            this.RackButton.TabIndex = 2;
            this.RackButton.UseVisualStyleBackColor = true;
            this.RackButton.Click += new System.EventHandler(this.RackButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(3, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(256, 256);
            this.PictureBox.TabIndex = 1;
            this.PictureBox.TabStop = false;
            // 
            // ImageHolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.RackButton);
            this.Controls.Add(this.RackSubButton);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.HolderLabel);
            this.Name = "ImageHolder";
            this.Size = new System.Drawing.Size(262, 290);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label HolderLabel;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button RackButton;
        private System.Windows.Forms.Button ViewButton;
        private System.Windows.Forms.Button RackSubButton;
    }
}
