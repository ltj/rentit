namespace ClientApp
{
    partial class Form1
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
            this.albumDetails1 = new ClientApp.AlbumDetails();
            this.mediaGrid = new ClientApp.MediaGrid();
            this.SuspendLayout();
            // 
            // albumDetails1
            // 
            this.albumDetails1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.albumDetails1.Location = new System.Drawing.Point(9, 12);
            this.albumDetails1.Name = "albumDetails1";
            this.albumDetails1.Size = new System.Drawing.Size(935, 572);
            this.albumDetails1.TabIndex = 0;
            // 
            // mediaGrid
            // 
            this.mediaGrid.Location = new System.Drawing.Point(0, 0);
            this.mediaGrid.Name = "mediaGrid";
            this.mediaGrid.Size = new System.Drawing.Size(462, 119);
            this.mediaGrid.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 596);
            this.Controls.Add(this.albumDetails1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private MediaGrid mediaGrid;

        #endregion
        private AlbumDetails albumDetails1;





























    }
}

