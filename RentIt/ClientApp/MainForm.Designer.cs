namespace ClientApp {
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            this.contentPane = new Panel();
            this.topBarControl = new TopBarControl();
            this.SuspendLayout();
            // 
            // contentPane
            // 
            this.contentPane.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.contentPane.Location = new Point(1, 76);
            this.contentPane.Name = "contentPane";
            this.contentPane.Size = new Size(769, 443);
            this.contentPane.TabIndex = 0;
            // 
            // topBarControl
            // 
            this.topBarControl.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.topBarControl.Location = new Point(1, 1);
            this.topBarControl.MaximumSize = new Size(99999, 76);
            this.topBarControl.MinimumSize = new Size(604, 76);
            this.topBarControl.Name = "topBarControl";
            this.topBarControl.Size = new Size(769, 76);
            this.topBarControl.TabIndex = 1;
            this.topBarControl.Title = "RentIt";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(770, 520);
            this.Controls.Add(this.topBarControl);
            this.Controls.Add(this.contentPane);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new Size(700, 500);
            this.Name = "MainForm";
            this.Text = "RentIt";
            this.ResumeLayout(false);

        }

        #endregion

        private Panel contentPane;
        private TopBarControl topBarControl;
    }
}