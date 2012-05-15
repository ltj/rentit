namespace ClientApp {
    partial class GenreList {
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
            this.lstGenres = new System.Windows.Forms.ListView();
            this.lblUCTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstGenres
            // 
            this.lstGenres.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGenres.BackColor = System.Drawing.SystemColors.Control;
            this.lstGenres.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGenres.Location = new System.Drawing.Point(3, 23);
            this.lstGenres.Name = "lstGenres";
            this.lstGenres.Size = new System.Drawing.Size(168, 219);
            this.lstGenres.TabIndex = 0;
            this.lstGenres.UseCompatibleStateImageBehavior = false;
            this.lstGenres.View = System.Windows.Forms.View.List;
            // 
            // lblUCTitle
            // 
            this.lblUCTitle.AutoSize = true;
            this.lblUCTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblUCTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUCTitle.Location = new System.Drawing.Point(-1, 0);
            this.lblUCTitle.Name = "lblUCTitle";
            this.lblUCTitle.Size = new System.Drawing.Size(68, 20);
            this.lblUCTitle.TabIndex = 1;
            this.lblUCTitle.Text = "Genres";
            // 
            // GenreList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblUCTitle);
            this.Controls.Add(this.lstGenres);
            this.Name = "GenreList";
            this.Size = new System.Drawing.Size(174, 245);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstGenres;
        private System.Windows.Forms.Label lblUCTitle;
    }
}
