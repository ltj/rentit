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
			this.creditsControl1 = new ClientApp.CreditsControl();
			this.mediaGrid = new ClientApp.MediaGrid();
			this.SuspendLayout();
			// 
			// creditsControl1
			// 
			this.creditsControl1.Location = new System.Drawing.Point(160, 73);
			this.creditsControl1.Name = "creditsControl1";
			this.creditsControl1.Size = new System.Drawing.Size(217, 232);
			this.creditsControl1.TabIndex = 0;
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
			this.ClientSize = new System.Drawing.Size(911, 596);
			this.Controls.Add(this.creditsControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

        }

        private MediaGrid mediaGrid;

        #endregion
		private CreditsControl creditsControl1;




















	}
}

