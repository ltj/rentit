namespace ClientControlLibrary
{
    partial class PublisherAccountManagement
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.AccountManagementTab = new System.Windows.Forms.TabPage();
            this.MediaManagementTab = new System.Windows.Forms.TabPage();
            this.MediaUploadTab = new System.Windows.Forms.TabPage();
            this.mediaUploadControl1 = new ClientControlLibrary.MediaUploadControl();
            this.TabControl.SuspendLayout();
            this.MediaUploadTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.AccountManagementTab);
            this.TabControl.Controls.Add(this.MediaManagementTab);
            this.TabControl.Controls.Add(this.MediaUploadTab);
            this.TabControl.Location = new System.Drawing.Point(3, 46);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(860, 591);
            this.TabControl.TabIndex = 0;
            // 
            // AccountManagementTab
            // 
            this.AccountManagementTab.Location = new System.Drawing.Point(4, 22);
            this.AccountManagementTab.Name = "AccountManagementTab";
            this.AccountManagementTab.Padding = new System.Windows.Forms.Padding(3);
            this.AccountManagementTab.Size = new System.Drawing.Size(852, 565);
            this.AccountManagementTab.TabIndex = 0;
            this.AccountManagementTab.Text = "Account Management";
            this.AccountManagementTab.UseVisualStyleBackColor = true;
            // 
            // MediaManagementTab
            // 
            this.MediaManagementTab.Location = new System.Drawing.Point(4, 22);
            this.MediaManagementTab.Name = "MediaManagementTab";
            this.MediaManagementTab.Padding = new System.Windows.Forms.Padding(3);
            this.MediaManagementTab.Size = new System.Drawing.Size(852, 468);
            this.MediaManagementTab.TabIndex = 1;
            this.MediaManagementTab.Text = "Media Management";
            this.MediaManagementTab.UseVisualStyleBackColor = true;
            // 
            // MediaUploadTab
            // 
            this.MediaUploadTab.Controls.Add(this.mediaUploadControl1);
            this.MediaUploadTab.Location = new System.Drawing.Point(4, 22);
            this.MediaUploadTab.Name = "MediaUploadTab";
            this.MediaUploadTab.Padding = new System.Windows.Forms.Padding(3);
            this.MediaUploadTab.Size = new System.Drawing.Size(852, 468);
            this.MediaUploadTab.TabIndex = 2;
            this.MediaUploadTab.Text = "Media Upload";
            this.MediaUploadTab.UseVisualStyleBackColor = true;
            // 
            // mediaUploadControl1
            // 
            this.mediaUploadControl1.Location = new System.Drawing.Point(-4, 0);
            this.mediaUploadControl1.Name = "mediaUploadControl1";
            this.mediaUploadControl1.Size = new System.Drawing.Size(400, 570);
            this.mediaUploadControl1.TabIndex = 0;
            // 
            // PublisherAccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "PublisherAccountManagement";
            this.Size = new System.Drawing.Size(866, 643);
            this.TabControl.ResumeLayout(false);
            this.MediaUploadTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage AccountManagementTab;
        private System.Windows.Forms.TabPage MediaManagementTab;
        private System.Windows.Forms.TabPage MediaUploadTab;
        private MediaUploadControl mediaUploadControl1;
    }
}
