namespace ClientApp
{
    partial class AlbumDetails
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
            this.mediaSideBar = new ClientApp.MediaSideBar();
            this.SuspendLayout();
            // 
            // songList
            // 
            this.songList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumn,
            this.artistColumn,
            this.genreColumn,
            this.priceColumn,
            this.trackLengthColumn});
            this.songList.Location = new System.Drawing.Point(240, 322);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(509, 170);
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
            this.songListLabel.Location = new System.Drawing.Point(236, 299);
            this.songListLabel.Name = "songListLabel";
            this.songListLabel.Size = new System.Drawing.Size(84, 20);
            this.songListLabel.TabIndex = 2;
            this.songListLabel.Text = "Song list:";
            // 
            // albumDescriptionTextBox
            // 
            this.albumDescriptionTextBox.Enabled = false;
            this.albumDescriptionTextBox.Location = new System.Drawing.Point(240, 187);
            this.albumDescriptionTextBox.Multiline = true;
            this.albumDescriptionTextBox.Name = "albumDescriptionTextBox";
            this.albumDescriptionTextBox.Size = new System.Drawing.Size(509, 77);
            this.albumDescriptionTextBox.TabIndex = 3;
            // 
            // albumDescription
            // 
            this.albumDescription.AutoSize = true;
            this.albumDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumDescription.Location = new System.Drawing.Point(236, 164);
            this.albumDescription.Name = "albumDescription";
            this.albumDescription.Size = new System.Drawing.Size(105, 20);
            this.albumDescription.TabIndex = 4;
            this.albumDescription.Text = "Description:";
            // 
            // albumTitleLabel
            // 
            this.albumTitleLabel.AutoSize = true;
            this.albumTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumTitleLabel.Location = new System.Drawing.Point(233, 34);
            this.albumTitleLabel.Name = "albumTitleLabel";
            this.albumTitleLabel.Size = new System.Drawing.Size(118, 39);
            this.albumTitleLabel.TabIndex = 5;
            this.albumTitleLabel.Text = "MDNA";
            // 
            // albumArtistLabel
            // 
            this.albumArtistLabel.AutoSize = true;
            this.albumArtistLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumArtistLabel.Location = new System.Drawing.Point(236, 73);
            this.albumArtistLabel.Name = "albumArtistLabel";
            this.albumArtistLabel.Size = new System.Drawing.Size(83, 20);
            this.albumArtistLabel.TabIndex = 6;
            this.albumArtistLabel.Text = "Madonna";
            // 
            // albumDurationLabel
            // 
            this.albumDurationLabel.AutoSize = true;
            this.albumDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumDurationLabel.Location = new System.Drawing.Point(237, 495);
            this.albumDurationLabel.Name = "albumDurationLabel";
            this.albumDurationLabel.Size = new System.Drawing.Size(90, 13);
            this.albumDurationLabel.TabIndex = 7;
            this.albumDurationLabel.Text = "Total duration:";
            // 
            // albumDurationValueLabel
            // 
            this.albumDurationValueLabel.AutoSize = true;
            this.albumDurationValueLabel.Location = new System.Drawing.Point(333, 495);
            this.albumDurationValueLabel.Name = "albumDurationValueLabel";
            this.albumDurationValueLabel.Size = new System.Drawing.Size(49, 13);
            this.albumDurationValueLabel.TabIndex = 8;
            this.albumDurationValueLabel.Text = "00:00:00";
            // 
            // mediaSideBar
            // 
            this.mediaSideBar.Location = new System.Drawing.Point(4, 4);
            this.mediaSideBar.Name = "mediaSideBar";
            this.mediaSideBar.Size = new System.Drawing.Size(211, 529);
            this.mediaSideBar.TabIndex = 0;
            // 
            // AlbumDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.albumDurationValueLabel);
            this.Controls.Add(this.albumDurationLabel);
            this.Controls.Add(this.albumArtistLabel);
            this.Controls.Add(this.albumTitleLabel);
            this.Controls.Add(this.albumDescription);
            this.Controls.Add(this.albumDescriptionTextBox);
            this.Controls.Add(this.songListLabel);
            this.Controls.Add(this.songList);
            this.Controls.Add(this.mediaSideBar);
            this.Name = "AlbumDetails";
            this.Size = new System.Drawing.Size(952, 536);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MediaSideBar mediaSideBar;
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
    }
}
