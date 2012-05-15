namespace ClientApp {
    partial class MoviePlayerControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoviePlayerControl));
            this.mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.titleLabel = new System.Windows.Forms.Label();
            this.viewMovieDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(3, 62);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaPlayer.OcxState")));
            this.mediaPlayer.Size = new System.Drawing.Size(633, 349);
            this.mediaPlayer.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(4, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(117, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Movie Title";
            // 
            // viewMovieDetails
            // 
            this.viewMovieDetails.Location = new System.Drawing.Point(9, 33);
            this.viewMovieDetails.Name = "viewMovieDetails";
            this.viewMovieDetails.Size = new System.Drawing.Size(82, 23);
            this.viewMovieDetails.TabIndex = 2;
            this.viewMovieDetails.Text = "Movie details";
            this.viewMovieDetails.UseVisualStyleBackColor = true;
            this.viewMovieDetails.Click += new System.EventHandler(this.viewMovieDetails_Click);
            // 
            // MoviePlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewMovieDetails);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.mediaPlayer);
            this.Name = "MoviePlayerControl";
            this.Size = new System.Drawing.Size(639, 414);
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button viewMovieDetails;
    }
}
