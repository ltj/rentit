namespace ClientApp
{
    partial class BookMovieDetails
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
            this.albumDescription = new System.Windows.Forms.Label();
            this.albumTitleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bookDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.userRatingsLabel = new System.Windows.Forms.Label();
            this.bookRatingList = new ClientApp.RatingList();
            this.mediaSideBar = new ClientApp.MediaSideBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // albumDescription
            // 
            this.albumDescription.AutoSize = true;
            this.albumDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumDescription.Location = new System.Drawing.Point(-4, 0);
            this.albumDescription.Name = "albumDescription";
            this.albumDescription.Size = new System.Drawing.Size(88, 20);
            this.albumDescription.TabIndex = 5;
            this.albumDescription.Text = "Summary:";
            // 
            // albumTitleLabel
            // 
            this.albumTitleLabel.AutoSize = true;
            this.albumTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumTitleLabel.Location = new System.Drawing.Point(234, 9);
            this.albumTitleLabel.Name = "albumTitleLabel";
            this.albumTitleLabel.Size = new System.Drawing.Size(118, 39);
            this.albumTitleLabel.TabIndex = 7;
            this.albumTitleLabel.Text = "MDNA";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorLabel.Location = new System.Drawing.Point(237, 48);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(83, 20);
            this.authorLabel.TabIndex = 8;
            this.authorLabel.Text = "Madonna";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.userRatingsLabel);
            this.panel1.Controls.Add(this.bookRatingList);
            this.panel1.Controls.Add(this.bookDescriptionTextBox);
            this.panel1.Controls.Add(this.albumDescription);
            this.panel1.Location = new System.Drawing.Point(241, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 402);
            this.panel1.TabIndex = 9;
            // 
            // bookDescriptionTextBox
            // 
            this.bookDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bookDescriptionTextBox.Enabled = false;
            this.bookDescriptionTextBox.Location = new System.Drawing.Point(3, 23);
            this.bookDescriptionTextBox.Multiline = true;
            this.bookDescriptionTextBox.Name = "bookDescriptionTextBox";
            this.bookDescriptionTextBox.Size = new System.Drawing.Size(553, 77);
            this.bookDescriptionTextBox.TabIndex = 6;
            // 
            // userRatingsLabel
            // 
            this.userRatingsLabel.AutoSize = true;
            this.userRatingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userRatingsLabel.Location = new System.Drawing.Point(6, 137);
            this.userRatingsLabel.Name = "userRatingsLabel";
            this.userRatingsLabel.Size = new System.Drawing.Size(112, 20);
            this.userRatingsLabel.TabIndex = 13;
            this.userRatingsLabel.Text = "User ratings:";
            // 
            // bookRatingList
            // 
            this.bookRatingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bookRatingList.Location = new System.Drawing.Point(3, 169);
            this.bookRatingList.MinimumSize = new System.Drawing.Size(300, 250);
            this.bookRatingList.Name = "bookRatingList";
            this.bookRatingList.Size = new System.Drawing.Size(553, 374);
            this.bookRatingList.TabIndex = 12;
            // 
            // mediaSideBar
            // 
            this.mediaSideBar.Location = new System.Drawing.Point(0, 0);
            this.mediaSideBar.Name = "mediaSideBar";
            this.mediaSideBar.Size = new System.Drawing.Size(211, 450);
            this.mediaSideBar.TabIndex = 6;
            // 
            // BookMovieDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.albumTitleLabel);
            this.Controls.Add(this.mediaSideBar);
            this.Name = "BookMovieDetails";
            this.Size = new System.Drawing.Size(986, 531);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label albumDescription;
        private MediaSideBar mediaSideBar;
        private System.Windows.Forms.Label albumTitleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox bookDescriptionTextBox;
        private System.Windows.Forms.Label userRatingsLabel;
        private RatingList bookRatingList;
    }
}
