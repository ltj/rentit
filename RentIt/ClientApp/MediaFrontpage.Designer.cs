namespace ClientApp {
    partial class MediaFrontpage {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.grpNewHot = new System.Windows.Forms.GroupBox();
            this.grpPopular = new System.Windows.Forms.GroupBox();
            this.picNewThumb = new System.Windows.Forms.PictureBox();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.lblNewRelease = new System.Windows.Forms.Label();
            this.lblNewGenre = new System.Windows.Forms.Label();
            this.lblNewPublisher = new System.Windows.Forms.Label();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.grpNewHot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNewThumb)).BeginInit();
            this.SuspendLayout();
            // 
            // grpNewHot
            // 
            this.grpNewHot.Controls.Add(this.lblNewPrice);
            this.grpNewHot.Controls.Add(this.lblNewPublisher);
            this.grpNewHot.Controls.Add(this.lblNewGenre);
            this.grpNewHot.Controls.Add(this.lblNewRelease);
            this.grpNewHot.Controls.Add(this.lblNewTitle);
            this.grpNewHot.Controls.Add(this.picNewThumb);
            this.grpNewHot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNewHot.Location = new System.Drawing.Point(3, 3);
            this.grpNewHot.Name = "grpNewHot";
            this.grpNewHot.Size = new System.Drawing.Size(679, 205);
            this.grpNewHot.TabIndex = 0;
            this.grpNewHot.TabStop = false;
            this.grpNewHot.Text = "New and HOT";
            // 
            // grpPopular
            // 
            this.grpPopular.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPopular.Location = new System.Drawing.Point(3, 214);
            this.grpPopular.Name = "grpPopular";
            this.grpPopular.Size = new System.Drawing.Size(679, 315);
            this.grpPopular.TabIndex = 1;
            this.grpPopular.TabStop = false;
            this.grpPopular.Text = "Most popular";
            // 
            // picNewThumb
            // 
            this.picNewThumb.Location = new System.Drawing.Point(502, 25);
            this.picNewThumb.Name = "picNewThumb";
            this.picNewThumb.Size = new System.Drawing.Size(160, 160);
            this.picNewThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNewThumb.TabIndex = 0;
            this.picNewThumb.TabStop = false;
            // 
            // lblNewTitle
            // 
            this.lblNewTitle.AutoSize = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewTitle.Location = new System.Drawing.Point(148, 25);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(39, 20);
            this.lblNewTitle.TabIndex = 1;
            this.lblNewTitle.Text = "title";
            // 
            // lblNewRelease
            // 
            this.lblNewRelease.AutoSize = true;
            this.lblNewRelease.Location = new System.Drawing.Point(148, 65);
            this.lblNewRelease.Name = "lblNewRelease";
            this.lblNewRelease.Size = new System.Drawing.Size(97, 20);
            this.lblNewRelease.TabIndex = 2;
            this.lblNewRelease.Text = "release date";
            // 
            // lblNewGenre
            // 
            this.lblNewGenre.AutoSize = true;
            this.lblNewGenre.Location = new System.Drawing.Point(148, 45);
            this.lblNewGenre.Name = "lblNewGenre";
            this.lblNewGenre.Size = new System.Drawing.Size(50, 20);
            this.lblNewGenre.TabIndex = 3;
            this.lblNewGenre.Text = "genre";
            // 
            // lblNewPublisher
            // 
            this.lblNewPublisher.AutoSize = true;
            this.lblNewPublisher.Location = new System.Drawing.Point(148, 105);
            this.lblNewPublisher.Name = "lblNewPublisher";
            this.lblNewPublisher.Size = new System.Drawing.Size(73, 20);
            this.lblNewPublisher.TabIndex = 4;
            this.lblNewPublisher.Text = "publisher";
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.AutoSize = true;
            this.lblNewPrice.Location = new System.Drawing.Point(148, 165);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(43, 20);
            this.lblNewPrice.TabIndex = 5;
            this.lblNewPrice.Text = "price";
            // 
            // MediaFrontpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.grpPopular);
            this.Controls.Add(this.grpNewHot);
            this.Name = "MediaFrontpage";
            this.Size = new System.Drawing.Size(701, 599);
            this.grpNewHot.ResumeLayout(false);
            this.grpNewHot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNewThumb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNewHot;
        private System.Windows.Forms.GroupBox grpPopular;
        private System.Windows.Forms.Label lblNewRelease;
        private System.Windows.Forms.Label lblNewTitle;
        private System.Windows.Forms.PictureBox picNewThumb;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Label lblNewPublisher;
        private System.Windows.Forms.Label lblNewGenre;
    }
}
