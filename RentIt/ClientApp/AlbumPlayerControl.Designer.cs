namespace ClientApp {
    partial class AlbumPlayerControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumPlayerControl));
            this.mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.songList = new ClientApp.PagedDetailedMediaListControl();
            this.titleLabel = new System.Windows.Forms.Label();
            this.viewAlbumDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(3, 410);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaPlayer.OcxState")));
            this.mediaPlayer.Size = new System.Drawing.Size(726, 45);
            this.mediaPlayer.TabIndex = 0;
            // 
            // songList
            // 
            this.songList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.songList.Location = new System.Drawing.Point(4, 32);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(725, 372);
            this.songList.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(3, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(119, 25);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Album Title";
            // 
            // viewAlbumDetails
            // 
            this.viewAlbumDetails.Location = new System.Drawing.Point(8, 32);
            this.viewAlbumDetails.Name = "viewAlbumDetails";
            this.viewAlbumDetails.Size = new System.Drawing.Size(86, 23);
            this.viewAlbumDetails.TabIndex = 3;
            this.viewAlbumDetails.Text = "Album details";
            this.viewAlbumDetails.UseVisualStyleBackColor = true;
            this.viewAlbumDetails.Click += new System.EventHandler(this.viewAlbumDetails_Click);
            // 
            // AlbumPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewAlbumDetails);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.songList);
            this.Controls.Add(this.mediaPlayer);
            this.Name = "AlbumPlayerControl";
            this.Size = new System.Drawing.Size(732, 458);
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private PagedDetailedMediaListControl songList;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button viewAlbumDetails;
    }
}
