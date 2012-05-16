namespace ClientApp
{
    internal partial class AlbumDetails
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
            this.songList = new System.Windows.Forms.ListView();
            this.titleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.genreColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackLengthColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.songListLabel = new System.Windows.Forms.Label();
            this.albumDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.albumDescription = new System.Windows.Forms.Label();
            this.albumTitleLabel = new System.Windows.Forms.Label();
            this.albumArtistLabel = new System.Windows.Forms.Label();
            this.albumDurationLabel = new System.Windows.Forms.Label();
            this.albumDurationValueLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.userRatingsLabel = new System.Windows.Forms.Label();
            this.albumRatingList = new ClientApp.RatingList();
            this.mediaSideBar = new ClientApp.MediaSideBar();
            this.alsoRentedList = new ClientApp.AlsoRentedList();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // songList
            // 
            this.songList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.songList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumn,
            this.artistColumn,
            this.genreColumn,
            this.priceColumn,
            this.trackLengthColumn});
            this.songList.Location = new System.Drawing.Point(3, 155);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(554, 199);
            this.songList.TabIndex = 1;
            this.songList.UseCompatibleStateImageBehavior = false;
            this.songList.View = System.Windows.Forms.View.Details;
            // 
            // titleColumn
            // 
            this.titleColumn.Text = "Title";
            this.titleColumn.Width = 142;
            // 
            // artistColumn
            // 
            this.artistColumn.Text = "Artist";
            this.artistColumn.Width = 124;
            // 
            // genreColumn
            // 
            this.genreColumn.Text = "Genre";
            this.genreColumn.Width = 94;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price";
            // 
            // trackLengthColumn
            // 
            this.trackLengthColumn.Text = "Track length";
            this.trackLengthColumn.Width = 74;
            // 
            // songListLabel
            // 
            this.songListLabel.AutoSize = true;
            this.songListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songListLabel.Location = new System.Drawing.Point(-1, 132);
            this.songListLabel.Name = "songListLabel";
            this.songListLabel.Size = new System.Drawing.Size(84, 20);
            this.songListLabel.TabIndex = 2;
            this.songListLabel.Text = "Song list:";
            // 
            // albumDescriptionTextBox
            // 
            this.albumDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.albumDescriptionTextBox.Enabled = false;
            this.albumDescriptionTextBox.Location = new System.Drawing.Point(4, 23);
            this.albumDescriptionTextBox.Multiline = true;
            this.albumDescriptionTextBox.Name = "albumDescriptionTextBox";
            this.albumDescriptionTextBox.Size = new System.Drawing.Size(553, 77);
            this.albumDescriptionTextBox.TabIndex = 3;
            // 
            // albumDescription
            // 
            this.albumDescription.AutoSize = true;
            this.albumDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumDescription.Location = new System.Drawing.Point(0, 0);
            this.albumDescription.Name = "albumDescription";
            this.albumDescription.Size = new System.Drawing.Size(105, 20);
            this.albumDescription.TabIndex = 4;
            this.albumDescription.Text = "Description:";
            // 
            // albumTitleLabel
            // 
            this.albumTitleLabel.AutoSize = true;
            this.albumTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumTitleLabel.Location = new System.Drawing.Point(233, 0);
            this.albumTitleLabel.Name = "albumTitleLabel";
            this.albumTitleLabel.Size = new System.Drawing.Size(118, 39);
            this.albumTitleLabel.TabIndex = 5;
            this.albumTitleLabel.Text = "MDNA";
            // 
            // albumArtistLabel
            // 
            this.albumArtistLabel.AutoSize = true;
            this.albumArtistLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumArtistLabel.Location = new System.Drawing.Point(236, 39);
            this.albumArtistLabel.Name = "albumArtistLabel";
            this.albumArtistLabel.Size = new System.Drawing.Size(83, 20);
            this.albumArtistLabel.TabIndex = 6;
            this.albumArtistLabel.Text = "Madonna";
            // 
            // albumDurationLabel
            // 
            this.albumDurationLabel.AutoSize = true;
            this.albumDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumDurationLabel.Location = new System.Drawing.Point(0, 357);
            this.albumDurationLabel.Name = "albumDurationLabel";
            this.albumDurationLabel.Size = new System.Drawing.Size(90, 13);
            this.albumDurationLabel.TabIndex = 7;
            this.albumDurationLabel.Text = "Total duration:";
            // 
            // albumDurationValueLabel
            // 
            this.albumDurationValueLabel.AutoSize = true;
            this.albumDurationValueLabel.Location = new System.Drawing.Point(96, 358);
            this.albumDurationValueLabel.Name = "albumDurationValueLabel";
            this.albumDurationValueLabel.Size = new System.Drawing.Size(49, 13);
            this.albumDurationValueLabel.TabIndex = 8;
            this.albumDurationValueLabel.Text = "00:00:00";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.userRatingsLabel);
            this.panel1.Controls.Add(this.albumDescription);
            this.panel1.Controls.Add(this.albumRatingList);
            this.panel1.Controls.Add(this.albumDescriptionTextBox);
            this.panel1.Controls.Add(this.albumDurationValueLabel);
            this.panel1.Controls.Add(this.albumDurationLabel);
            this.panel1.Controls.Add(this.songListLabel);
            this.panel1.Controls.Add(this.songList);
            this.panel1.Location = new System.Drawing.Point(240, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 569);
            this.panel1.TabIndex = 9;
            // 
            // userRatingsLabel
            // 
            this.userRatingsLabel.AutoSize = true;
            this.userRatingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userRatingsLabel.Location = new System.Drawing.Point(0, 413);
            this.userRatingsLabel.Name = "userRatingsLabel";
            this.userRatingsLabel.Size = new System.Drawing.Size(112, 20);
            this.userRatingsLabel.TabIndex = 11;
            this.userRatingsLabel.Text = "User ratings:";
            // 
            // albumRatingList
            // 
            this.albumRatingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.albumRatingList.Location = new System.Drawing.Point(-3, 445);
            this.albumRatingList.MinimumSize = new System.Drawing.Size(300, 250);
            this.albumRatingList.Name = "albumRatingList";
            this.albumRatingList.Size = new System.Drawing.Size(560, 374);
            this.albumRatingList.TabIndex = 9;
            // 
            // mediaSideBar
            // 
            this.mediaSideBar.Location = new System.Drawing.Point(0, 0);
            this.mediaSideBar.Name = "mediaSideBar";
            this.mediaSideBar.Size = new System.Drawing.Size(211, 504);
            this.mediaSideBar.TabIndex = 10;
            // 
            // alsoRentedList
            // 
            this.alsoRentedList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alsoRentedList.Location = new System.Drawing.Point(823, 62);
            this.alsoRentedList.Name = "alsoRentedList";
            this.alsoRentedList.Size = new System.Drawing.Size(154, 294);
            this.alsoRentedList.TabIndex = 11;
            // 
            // AlbumDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.alsoRentedList);
            this.Controls.Add(this.mediaSideBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.albumArtistLabel);
            this.Controls.Add(this.albumTitleLabel);
            this.Name = "AlbumDetails";
            this.Size = new System.Drawing.Size(980, 634);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView songList;
        private System.Windows.Forms.ColumnHeader titleColumn;
        private System.Windows.Forms.ColumnHeader artistColumn;
        private System.Windows.Forms.ColumnHeader genreColumn;
        private System.Windows.Forms.ColumnHeader trackLengthColumn;
        private System.Windows.Forms.ColumnHeader priceColumn;
        private System.Windows.Forms.Label songListLabel;
        private System.Windows.Forms.TextBox albumDescriptionTextBox;
        private System.Windows.Forms.Label albumDescription;
        private System.Windows.Forms.Label albumTitleLabel;
        private System.Windows.Forms.Label albumArtistLabel;
        private System.Windows.Forms.Label albumDurationLabel;
        private System.Windows.Forms.Label albumDurationValueLabel;
        private System.Windows.Forms.Panel panel1;
        private RatingList albumRatingList;
        private System.Windows.Forms.Label userRatingsLabel;
        private MediaSideBar mediaSideBar;
        private AlsoRentedList alsoRentedList;
    }
}
