﻿namespace ClientApp
{
    partial class MediaSideBar
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
            this.thumbnailBox = new System.Windows.Forms.PictureBox();
            this.rentButton = new System.Windows.Forms.Button();
            this.priceLabel = new System.Windows.Forms.Label();
            this.genreLabel = new System.Windows.Forms.Label();
            this.releaseDateLabel = new System.Windows.Forms.Label();
            this.publisherLabel = new System.Windows.Forms.Label();
            this.genreValueLabel = new System.Windows.Forms.Label();
            this.releaseDateValueLabel = new System.Windows.Forms.Label();
            this.originLabel = new System.Windows.Forms.Label();
            this.originValueLabel = new System.Windows.Forms.Label();
            this.publisherValueLabel = new System.Windows.Forms.Label();
            this.mediaDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.lengthValueLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.rentalPeriodLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailBox)).BeginInit();
            this.mediaDetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // thumbnailBox
            // 
            this.thumbnailBox.Location = new System.Drawing.Point(3, 3);
            this.thumbnailBox.Name = "thumbnailBox";
            this.thumbnailBox.Size = new System.Drawing.Size(204, 196);
            this.thumbnailBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumbnailBox.TabIndex = 1;
            this.thumbnailBox.TabStop = false;
            // 
            // rentButton
            // 
            this.rentButton.Location = new System.Drawing.Point(4, 220);
            this.rentButton.Name = "rentButton";
            this.rentButton.Size = new System.Drawing.Size(96, 25);
            this.rentButton.TabIndex = 2;
            this.rentButton.Text = "Rent Media";
            this.rentButton.UseVisualStyleBackColor = true;
            this.rentButton.Click += new System.EventHandler(this.rentButton_Click);
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(106, 232);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(56, 13);
            this.priceLabel.TabIndex = 3;
            this.priceLabel.Text = "priceLabel";
            // 
            // genreLabel
            // 
            this.genreLabel.AutoSize = true;
            this.genreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genreLabel.Location = new System.Drawing.Point(15, 26);
            this.genreLabel.Name = "genreLabel";
            this.genreLabel.Size = new System.Drawing.Size(45, 13);
            this.genreLabel.TabIndex = 5;
            this.genreLabel.Text = "Genre:";
            // 
            // releaseDateLabel
            // 
            this.releaseDateLabel.AutoSize = true;
            this.releaseDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.releaseDateLabel.Location = new System.Drawing.Point(15, 108);
            this.releaseDateLabel.Name = "releaseDateLabel";
            this.releaseDateLabel.Size = new System.Drawing.Size(86, 13);
            this.releaseDateLabel.TabIndex = 6;
            this.releaseDateLabel.Text = "Release date:";
            // 
            // publisherLabel
            // 
            this.publisherLabel.AutoSize = true;
            this.publisherLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.publisherLabel.Location = new System.Drawing.Point(15, 151);
            this.publisherLabel.Name = "publisherLabel";
            this.publisherLabel.Size = new System.Drawing.Size(63, 13);
            this.publisherLabel.TabIndex = 7;
            this.publisherLabel.Text = "Publisher:";
            // 
            // genreValueLabel
            // 
            this.genreValueLabel.AutoSize = true;
            this.genreValueLabel.Location = new System.Drawing.Point(15, 39);
            this.genreValueLabel.Name = "genreValueLabel";
            this.genreValueLabel.Size = new System.Drawing.Size(63, 13);
            this.genreValueLabel.TabIndex = 8;
            this.genreValueLabel.Text = "genre value";
            // 
            // releaseDateValueLabel
            // 
            this.releaseDateValueLabel.AutoSize = true;
            this.releaseDateValueLabel.Location = new System.Drawing.Point(15, 121);
            this.releaseDateValueLabel.Name = "releaseDateValueLabel";
            this.releaseDateValueLabel.Size = new System.Drawing.Size(35, 13);
            this.releaseDateValueLabel.TabIndex = 9;
            this.releaseDateValueLabel.Text = "label1";
            // 
            // originLabel
            // 
            this.originLabel.AutoSize = true;
            this.originLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originLabel.Location = new System.Drawing.Point(15, 65);
            this.originLabel.Name = "originLabel";
            this.originLabel.Size = new System.Drawing.Size(69, 13);
            this.originLabel.TabIndex = 10;
            this.originLabel.Text = "originLabel";
            // 
            // originValueLabel
            // 
            this.originValueLabel.AutoSize = true;
            this.originValueLabel.Location = new System.Drawing.Point(15, 78);
            this.originValueLabel.Name = "originValueLabel";
            this.originValueLabel.Size = new System.Drawing.Size(61, 13);
            this.originValueLabel.TabIndex = 11;
            this.originValueLabel.Text = "origin value";
            // 
            // publisherValueLabel
            // 
            this.publisherValueLabel.AutoSize = true;
            this.publisherValueLabel.Location = new System.Drawing.Point(15, 164);
            this.publisherValueLabel.Name = "publisherValueLabel";
            this.publisherValueLabel.Size = new System.Drawing.Size(78, 13);
            this.publisherValueLabel.TabIndex = 12;
            this.publisherValueLabel.Text = "publisher value";
            // 
            // mediaDetailsGroupBox
            // 
            this.mediaDetailsGroupBox.Controls.Add(this.lengthValueLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.lengthLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.genreLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.publisherValueLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.releaseDateLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.originValueLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.publisherLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.originLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.genreValueLabel);
            this.mediaDetailsGroupBox.Controls.Add(this.releaseDateValueLabel);
            this.mediaDetailsGroupBox.Location = new System.Drawing.Point(3, 263);
            this.mediaDetailsGroupBox.Name = "mediaDetailsGroupBox";
            this.mediaDetailsGroupBox.Size = new System.Drawing.Size(204, 238);
            this.mediaDetailsGroupBox.TabIndex = 13;
            this.mediaDetailsGroupBox.TabStop = false;
            this.mediaDetailsGroupBox.Text = "Media Details";
            // 
            // lengthValueLabel
            // 
            this.lengthValueLabel.AutoSize = true;
            this.lengthValueLabel.Location = new System.Drawing.Point(15, 209);
            this.lengthValueLabel.Name = "lengthValueLabel";
            this.lengthValueLabel.Size = new System.Drawing.Size(89, 13);
            this.lengthValueLabel.TabIndex = 14;
            this.lengthValueLabel.Text = "lengthValueLabel";
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lengthLabel.Location = new System.Drawing.Point(15, 196);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(73, 13);
            this.lengthLabel.TabIndex = 13;
            this.lengthLabel.Text = "lengthLabel";
            // 
            // rentalPeriodLabel
            // 
            this.rentalPeriodLabel.AutoSize = true;
            this.rentalPeriodLabel.Location = new System.Drawing.Point(106, 219);
            this.rentalPeriodLabel.Name = "rentalPeriodLabel";
            this.rentalPeriodLabel.Size = new System.Drawing.Size(76, 13);
            this.rentalPeriodLabel.TabIndex = 14;
            this.rentalPeriodLabel.Text = "14-days rental,";
            // 
            // MediaSideBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rentalPeriodLabel);
            this.Controls.Add(this.mediaDetailsGroupBox);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.rentButton);
            this.Controls.Add(this.thumbnailBox);
            this.Name = "MediaSideBar";
            this.Size = new System.Drawing.Size(211, 504);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailBox)).EndInit();
            this.mediaDetailsGroupBox.ResumeLayout(false);
            this.mediaDetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox thumbnailBox;
        private System.Windows.Forms.Button rentButton;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label genreLabel;
        private System.Windows.Forms.Label releaseDateLabel;
        private System.Windows.Forms.Label publisherLabel;
        private System.Windows.Forms.Label genreValueLabel;
        private System.Windows.Forms.Label releaseDateValueLabel;
        private System.Windows.Forms.Label originLabel;
        private System.Windows.Forms.Label originValueLabel;
        private System.Windows.Forms.Label publisherValueLabel;
        private System.Windows.Forms.GroupBox mediaDetailsGroupBox;
        private System.Windows.Forms.Label lengthValueLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label rentalPeriodLabel;
    }
}
