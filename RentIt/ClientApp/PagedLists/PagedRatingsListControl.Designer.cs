namespace ClientApp
{
    partial class PagedRatingsListControl
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
            this.ratingsList = new ClientApp.PagedRatingsList();
            this.currentPageTextbox = new System.Windows.Forms.TextBox();
            this.lastPageButton = new System.Windows.Forms.Button();
            this.nextPageButton = new System.Windows.Forms.Button();
            this.previousPageButton = new System.Windows.Forms.Button();
            this.firstPageButton = new System.Windows.Forms.Button();
            this.itemsPerPageLabel = new System.Windows.Forms.Label();
            this.itemsPerPageComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.qouteLabel = new System.Windows.Forms.Label();
            this.fullTextReviewTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ratingsList
            // 
            this.ratingsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ratingsList.AutoArrange = false;
            this.ratingsList.FullRowSelect = true;
            this.ratingsList.GridLines = true;
            this.ratingsList.Location = new System.Drawing.Point(3, 41);
            this.ratingsList.MultiSelect = false;
            this.ratingsList.Name = "ratingsList";
            this.ratingsList.Size = new System.Drawing.Size(735, 254);
            this.ratingsList.TabIndex = 9;
            this.ratingsList.UseCompatibleStateImageBehavior = false;
            this.ratingsList.View = System.Windows.Forms.View.Details;
            // 
            // currentPageTextbox
            // 
            this.currentPageTextbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.currentPageTextbox.Location = new System.Drawing.Point(344, 301);
            this.currentPageTextbox.Name = "currentPageTextbox";
            this.currentPageTextbox.ReadOnly = true;
            this.currentPageTextbox.Size = new System.Drawing.Size(55, 20);
            this.currentPageTextbox.TabIndex = 16;
            this.currentPageTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lastPageButton
            // 
            this.lastPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lastPageButton.Location = new System.Drawing.Point(499, 301);
            this.lastPageButton.Name = "lastPageButton";
            this.lastPageButton.Size = new System.Drawing.Size(75, 23);
            this.lastPageButton.TabIndex = 15;
            this.lastPageButton.Text = "Last";
            this.lastPageButton.UseVisualStyleBackColor = true;
            this.lastPageButton.Click += new System.EventHandler(this.lastPageButton_Click);
            // 
            // nextPageButton
            // 
            this.nextPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.nextPageButton.Location = new System.Drawing.Point(418, 301);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(75, 23);
            this.nextPageButton.TabIndex = 14;
            this.nextPageButton.Text = "Next";
            this.nextPageButton.UseVisualStyleBackColor = true;
            this.nextPageButton.Click += new System.EventHandler(this.nextPageButton_Click);
            // 
            // previousPageButton
            // 
            this.previousPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.previousPageButton.Location = new System.Drawing.Point(251, 301);
            this.previousPageButton.Name = "previousPageButton";
            this.previousPageButton.Size = new System.Drawing.Size(75, 23);
            this.previousPageButton.TabIndex = 13;
            this.previousPageButton.Text = "Previous";
            this.previousPageButton.UseVisualStyleBackColor = true;
            this.previousPageButton.Click += new System.EventHandler(this.previousPageButton_Click);
            // 
            // firstPageButton
            // 
            this.firstPageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.firstPageButton.Location = new System.Drawing.Point(170, 301);
            this.firstPageButton.Name = "firstPageButton";
            this.firstPageButton.Size = new System.Drawing.Size(75, 23);
            this.firstPageButton.TabIndex = 12;
            this.firstPageButton.Text = "First";
            this.firstPageButton.UseVisualStyleBackColor = true;
            this.firstPageButton.Click += new System.EventHandler(this.firstPageButton_Click);
            // 
            // itemsPerPageLabel
            // 
            this.itemsPerPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsPerPageLabel.AutoSize = true;
            this.itemsPerPageLabel.Location = new System.Drawing.Point(541, 12);
            this.itemsPerPageLabel.Name = "itemsPerPageLabel";
            this.itemsPerPageLabel.Size = new System.Drawing.Size(80, 13);
            this.itemsPerPageLabel.TabIndex = 11;
            this.itemsPerPageLabel.Text = "Items per page:";
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
            this.itemsPerPageComboBox.Location = new System.Drawing.Point(627, 9);
            this.itemsPerPageComboBox.Name = "itemsPerPageComboBox";
            this.itemsPerPageComboBox.Size = new System.Drawing.Size(103, 21);
            this.itemsPerPageComboBox.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ratingsList);
            this.panel1.Controls.Add(this.currentPageTextbox);
            this.panel1.Controls.Add(this.itemsPerPageLabel);
            this.panel1.Controls.Add(this.lastPageButton);
            this.panel1.Controls.Add(this.itemsPerPageComboBox);
            this.panel1.Controls.Add(this.nextPageButton);
            this.panel1.Controls.Add(this.previousPageButton);
            this.panel1.Controls.Add(this.firstPageButton);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(741, 327);
            this.panel1.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.qouteLabel);
            this.groupBox1.Controls.Add(this.fullTextReviewTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 163);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Complete review text of selected review";
            // 
            // qouteLabel
            // 
            this.qouteLabel.AutoSize = true;
            this.qouteLabel.Font = new System.Drawing.Font("Calibri", 80F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qouteLabel.Location = new System.Drawing.Point(3, 16);
            this.qouteLabel.Name = "qouteLabel";
            this.qouteLabel.Size = new System.Drawing.Size(102, 131);
            this.qouteLabel.TabIndex = 2;
            this.qouteLabel.Text = "\"";
            // 
            // fullTextReviewTextBox
            // 
            this.fullTextReviewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fullTextReviewTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fullTextReviewTextBox.Enabled = false;
            this.fullTextReviewTextBox.Location = new System.Drawing.Point(106, 28);
            this.fullTextReviewTextBox.Multiline = true;
            this.fullTextReviewTextBox.Name = "fullTextReviewTextBox";
            this.fullTextReviewTextBox.Size = new System.Drawing.Size(618, 110);
            this.fullTextReviewTextBox.TabIndex = 1;
            // 
            // PagedRatingsListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "PagedRatingsListControl";
            this.Size = new System.Drawing.Size(747, 512);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox itemsPerPageComboBox;
        private System.Windows.Forms.Label itemsPerPageLabel;
        private System.Windows.Forms.Button firstPageButton;
        private System.Windows.Forms.Button previousPageButton;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.Button lastPageButton;
        private System.Windows.Forms.TextBox currentPageTextbox;
        private PagedRatingsList ratingsList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fullTextReviewTextBox;
        private System.Windows.Forms.Label qouteLabel;
    }
}
