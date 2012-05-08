namespace ClientApp
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Songs", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Albums", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Movies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Books", System.Windows.Forms.HorizontalAlignment.Left);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.AccountManagementTab = new System.Windows.Forms.TabPage();
            this.MediaManagementTab = new System.Windows.Forms.TabPage();
            this.changePriceButton = new System.Windows.Forms.Button();
            this.deleteMediaButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MediaUploadTab = new System.Windows.Forms.TabPage();
            this.mediaUploadControl1 = new ClientApp.MediaUploadControl();
            this.publishedMediaList = new ClientApp.MediaList();
            this.TabControl.SuspendLayout();
            this.MediaManagementTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MediaUploadTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.AccountManagementTab);
            this.TabControl.Controls.Add(this.MediaManagementTab);
            this.TabControl.Controls.Add(this.MediaUploadTab);
            this.TabControl.Location = new System.Drawing.Point(3, 3);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(860, 557);
            this.TabControl.TabIndex = 0;
            // 
            // AccountManagementTab
            // 
            this.AccountManagementTab.Location = new System.Drawing.Point(4, 22);
            this.AccountManagementTab.Name = "AccountManagementTab";
            this.AccountManagementTab.Padding = new System.Windows.Forms.Padding(3);
            this.AccountManagementTab.Size = new System.Drawing.Size(852, 531);
            this.AccountManagementTab.TabIndex = 0;
            this.AccountManagementTab.Text = "Account Management";
            this.AccountManagementTab.UseVisualStyleBackColor = true;
            // 
            // MediaManagementTab
            // 
            this.MediaManagementTab.Controls.Add(this.changePriceButton);
            this.MediaManagementTab.Controls.Add(this.deleteMediaButton);
            this.MediaManagementTab.Controls.Add(this.panel1);
            this.MediaManagementTab.Location = new System.Drawing.Point(4, 22);
            this.MediaManagementTab.Name = "MediaManagementTab";
            this.MediaManagementTab.Padding = new System.Windows.Forms.Padding(3);
            this.MediaManagementTab.Size = new System.Drawing.Size(852, 531);
            this.MediaManagementTab.TabIndex = 1;
            this.MediaManagementTab.Text = "Media Management";
            this.MediaManagementTab.UseVisualStyleBackColor = true;
            // 
            // changePriceButton
            // 
            this.changePriceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changePriceButton.Location = new System.Drawing.Point(741, 476);
            this.changePriceButton.Name = "changePriceButton";
            this.changePriceButton.Size = new System.Drawing.Size(91, 40);
            this.changePriceButton.TabIndex = 16;
            this.changePriceButton.Text = "Change price";
            this.changePriceButton.UseVisualStyleBackColor = true;
            // 
            // deleteMediaButton
            // 
            this.deleteMediaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteMediaButton.Location = new System.Drawing.Point(741, 431);
            this.deleteMediaButton.Name = "deleteMediaButton";
            this.deleteMediaButton.Size = new System.Drawing.Size(91, 39);
            this.deleteMediaButton.TabIndex = 15;
            this.deleteMediaButton.Text = "Delete selected media";
            this.deleteMediaButton.UseVisualStyleBackColor = true;
            this.deleteMediaButton.Click += new System.EventHandler(this.deleteMediaButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.publishedMediaList);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 519);
            this.panel1.TabIndex = 14;
            // 
            // MediaUploadTab
            // 
            this.MediaUploadTab.Controls.Add(this.mediaUploadControl1);
            this.MediaUploadTab.Location = new System.Drawing.Point(4, 22);
            this.MediaUploadTab.Name = "MediaUploadTab";
            this.MediaUploadTab.Padding = new System.Windows.Forms.Padding(3);
            this.MediaUploadTab.Size = new System.Drawing.Size(852, 531);
            this.MediaUploadTab.TabIndex = 2;
            this.MediaUploadTab.Text = "Media Upload";
            this.MediaUploadTab.UseVisualStyleBackColor = true;
            // 
            // mediaUploadControl1
            // 
            this.mediaUploadControl1.Location = new System.Drawing.Point(7, 7);
            this.mediaUploadControl1.Name = "mediaUploadControl1";
            this.mediaUploadControl1.Size = new System.Drawing.Size(812, 519);
            this.mediaUploadControl1.TabIndex = 0;
            // 
            // publishedMediaList
            // 
            this.publishedMediaList.AutoArrange = false;
            this.publishedMediaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publishedMediaList.FullRowSelect = true;
            this.publishedMediaList.GridLines = true;
            listViewGroup1.Header = "Songs";
            listViewGroup1.Name = null;
            listViewGroup2.Header = "Albums";
            listViewGroup2.Name = null;
            listViewGroup3.Header = "Movies";
            listViewGroup3.Name = null;
            listViewGroup4.Header = "Books";
            listViewGroup4.Name = null;
            this.publishedMediaList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.publishedMediaList.Location = new System.Drawing.Point(0, 0);
            this.publishedMediaList.MultiSelect = false;
            this.publishedMediaList.Name = "publishedMediaList";
            this.publishedMediaList.Size = new System.Drawing.Size(716, 519);
            this.publishedMediaList.TabIndex = 9;
            this.publishedMediaList.UseCompatibleStateImageBehavior = false;
            this.publishedMediaList.View = System.Windows.Forms.View.Details;
            // 
            // PublisherAccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "PublisherAccountManagement";
            this.Size = new System.Drawing.Size(866, 564);
            this.TabControl.ResumeLayout(false);
            this.MediaManagementTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.MediaUploadTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage AccountManagementTab;
        private System.Windows.Forms.TabPage MediaUploadTab;
        private MediaUploadControl mediaUploadControl1;
        private System.Windows.Forms.TabPage MediaManagementTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button changePriceButton;
        private System.Windows.Forms.Button deleteMediaButton;
        private MediaList publishedMediaList;
    }
}
