namespace ClientApp
{
    partial class RentalsListControl
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
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Songs", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Albums", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Movies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Books", System.Windows.Forms.HorizontalAlignment.Left);
            this.itemsPerPageComboBox = new System.Windows.Forms.ComboBox();
            this.itemsPerPageLabel = new System.Windows.Forms.Label();
            this.firstPageButton = new System.Windows.Forms.Button();
            this.previousPageButton = new System.Windows.Forms.Button();
            this.nextPageButton = new System.Windows.Forms.Button();
            this.lastPageButton = new System.Windows.Forms.Button();
            this.currentPageTextbox = new System.Windows.Forms.TextBox();
            this.mediaList = new ClientApp.RentalsList();
            this.SuspendLayout();
            // 
            // itemsPerPageComboBox
            // 
            this.itemsPerPageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsPerPageComboBox.FormattingEnabled = true;
            this.itemsPerPageComboBox.Items.AddRange(new object[] {
            "10",
            "25",
            "50",
            "100"});
            this.itemsPerPageComboBox.Location = new System.Drawing.Point(411, 8);
            this.itemsPerPageComboBox.Name = "itemsPerPageComboBox";
            this.itemsPerPageComboBox.Size = new System.Drawing.Size(103, 21);
            this.itemsPerPageComboBox.TabIndex = 10;
            // 
            // itemsPerPageLabel
            // 
            this.itemsPerPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsPerPageLabel.AutoSize = true;
            this.itemsPerPageLabel.Location = new System.Drawing.Point(325, 11);
            this.itemsPerPageLabel.Name = "itemsPerPageLabel";
            this.itemsPerPageLabel.Size = new System.Drawing.Size(80, 13);
            this.itemsPerPageLabel.TabIndex = 11;
            this.itemsPerPageLabel.Text = "Items per page:";
            // 
            // firstPageButton
            // 
            this.firstPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.firstPageButton.Location = new System.Drawing.Point(67, 419);
            this.firstPageButton.Name = "firstPageButton";
            this.firstPageButton.Size = new System.Drawing.Size(75, 23);
            this.firstPageButton.TabIndex = 12;
            this.firstPageButton.Text = "First";
            this.firstPageButton.UseVisualStyleBackColor = true;
            this.firstPageButton.Click += new System.EventHandler(this.firstPageButton_Click);
            // 
            // previousPageButton
            // 
            this.previousPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.previousPageButton.Location = new System.Drawing.Point(148, 419);
            this.previousPageButton.Name = "previousPageButton";
            this.previousPageButton.Size = new System.Drawing.Size(75, 23);
            this.previousPageButton.TabIndex = 13;
            this.previousPageButton.Text = "Previous";
            this.previousPageButton.UseVisualStyleBackColor = true;
            this.previousPageButton.Click += new System.EventHandler(this.previousPageButton_Click);
            // 
            // nextPageButton
            // 
            this.nextPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.nextPageButton.Location = new System.Drawing.Point(315, 419);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(75, 23);
            this.nextPageButton.TabIndex = 14;
            this.nextPageButton.Text = "Next";
            this.nextPageButton.UseVisualStyleBackColor = true;
            this.nextPageButton.Click += new System.EventHandler(this.nextPageButton_Click);
            // 
            // lastPageButton
            // 
            this.lastPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lastPageButton.Location = new System.Drawing.Point(396, 419);
            this.lastPageButton.Name = "lastPageButton";
            this.lastPageButton.Size = new System.Drawing.Size(75, 23);
            this.lastPageButton.TabIndex = 15;
            this.lastPageButton.Text = "Last";
            this.lastPageButton.UseVisualStyleBackColor = true;
            this.lastPageButton.Click += new System.EventHandler(this.lastPageButton_Click);
            // 
            // currentPageTextbox
            // 
            this.currentPageTextbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.currentPageTextbox.Location = new System.Drawing.Point(241, 419);
            this.currentPageTextbox.Name = "currentPageTextbox";
            this.currentPageTextbox.ReadOnly = true;
            this.currentPageTextbox.Size = new System.Drawing.Size(55, 20);
            this.currentPageTextbox.TabIndex = 16;
            this.currentPageTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mediaList
            // 
            this.mediaList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaList.AutoArrange = false;
            this.mediaList.FullRowSelect = true;
            this.mediaList.GridLines = true;
            listViewGroup1.Header = "Songs";
            listViewGroup1.Name = null;
            listViewGroup2.Header = "Albums";
            listViewGroup2.Name = null;
            listViewGroup3.Header = "Movies";
            listViewGroup3.Name = null;
            listViewGroup4.Header = "Books";
            listViewGroup4.Name = null;
            listViewGroup5.Header = "Songs";
            listViewGroup5.Name = null;
            listViewGroup6.Header = "Albums";
            listViewGroup6.Name = null;
            listViewGroup7.Header = "Movies";
            listViewGroup7.Name = null;
            listViewGroup8.Header = "Books";
            listViewGroup8.Name = null;
            this.mediaList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8});
            this.mediaList.Location = new System.Drawing.Point(4, 35);
            this.mediaList.MultiSelect = false;
            this.mediaList.Name = "mediaList";
            this.mediaList.Size = new System.Drawing.Size(510, 378);
            this.mediaList.TabIndex = 9;
            this.mediaList.UseCompatibleStateImageBehavior = false;
            this.mediaList.View = System.Windows.Forms.View.Details;
            // 
            // RentalsListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mediaList);
            this.Controls.Add(this.currentPageTextbox);
            this.Controls.Add(this.lastPageButton);
            this.Controls.Add(this.nextPageButton);
            this.Controls.Add(this.previousPageButton);
            this.Controls.Add(this.firstPageButton);
            this.Controls.Add(this.itemsPerPageLabel);
            this.Controls.Add(this.itemsPerPageComboBox);
            this.Name = "RentalsListControl";
            this.Size = new System.Drawing.Size(517, 442);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox itemsPerPageComboBox;
        private System.Windows.Forms.Label itemsPerPageLabel;
        private System.Windows.Forms.Button firstPageButton;
        private System.Windows.Forms.Button previousPageButton;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.Button lastPageButton;
        private System.Windows.Forms.TextBox currentPageTextbox;
        private RentalsList mediaList;
    }
}
