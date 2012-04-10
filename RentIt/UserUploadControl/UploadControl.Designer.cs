namespace PropertyGridTest
{
    partial class UploadControl
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
            this.songPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.newButton = new System.Windows.Forms.Button();
            this.songListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.mediaPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.songsGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteAllItemsButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.metaDataLabel = new System.Windows.Forms.Label();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.songsGroupBox.SuspendLayout();
            this.mainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // songPropertyGrid
            // 
            this.songPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.songPropertyGrid.Location = new System.Drawing.Point(214, 19);
            this.songPropertyGrid.Name = "songPropertyGrid";
            this.songPropertyGrid.Size = new System.Drawing.Size(311, 275);
            this.songPropertyGrid.TabIndex = 0;
            this.songPropertyGrid.ToolbarVisible = false;
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newButton.Location = new System.Drawing.Point(450, 296);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 50);
            this.newButton.TabIndex = 2;
            this.newButton.Text = "New song";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newSongButton_Click);
            // 
            // songListView
            // 
            this.songListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.songListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.songListView.FullRowSelect = true;
            this.songListView.GridLines = true;
            this.songListView.Location = new System.Drawing.Point(6, 19);
            this.songListView.Name = "songListView";
            this.songListView.ShowItemToolTips = true;
            this.songListView.Size = new System.Drawing.Size(202, 327);
            this.songListView.TabIndex = 3;
            this.songListView.UseCompatibleStateImageBehavior = false;
            this.songListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Song title";
            this.columnHeader1.Width = 197;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "Book",
            "Movie",
            "Album"});
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Items.AddRange(new object[] {
            "Book",
            "Album",
            "Movie"});
            this.comboBox1.Location = new System.Drawing.Point(6, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(187, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // mediaPropertyGrid
            // 
            this.mediaPropertyGrid.Location = new System.Drawing.Point(6, 125);
            this.mediaPropertyGrid.Name = "mediaPropertyGrid";
            this.mediaPropertyGrid.Size = new System.Drawing.Size(220, 346);
            this.mediaPropertyGrid.TabIndex = 6;
            this.mediaPropertyGrid.ToolbarVisible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "1. Select media type to upload:";
            // 
            // songsGroupBox
            // 
            this.songsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.songsGroupBox.Controls.Add(this.deleteAllItemsButton);
            this.songsGroupBox.Controls.Add(this.songListView);
            this.songsGroupBox.Controls.Add(this.songPropertyGrid);
            this.songsGroupBox.Controls.Add(this.deleteSelectedButton);
            this.songsGroupBox.Controls.Add(this.newButton);
            this.songsGroupBox.Location = new System.Drawing.Point(232, 125);
            this.songsGroupBox.Name = "songsGroupBox";
            this.songsGroupBox.Size = new System.Drawing.Size(540, 350);
            this.songsGroupBox.TabIndex = 8;
            this.songsGroupBox.TabStop = false;
            this.songsGroupBox.Text = "Album songs";
            this.songsGroupBox.Visible = false;
            // 
            // deleteAllItemsButton
            // 
            this.deleteAllItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllItemsButton.Location = new System.Drawing.Point(288, 296);
            this.deleteAllItemsButton.Name = "deleteAllItemsButton";
            this.deleteAllItemsButton.Size = new System.Drawing.Size(75, 50);
            this.deleteAllItemsButton.TabIndex = 5;
            this.deleteAllItemsButton.Text = "Delete all songs";
            this.deleteAllItemsButton.UseVisualStyleBackColor = true;
            this.deleteAllItemsButton.Click += new System.EventHandler(this.deleteAllSongsButton_Click);
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelectedButton.Location = new System.Drawing.Point(369, 296);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(75, 50);
            this.deleteSelectedButton.TabIndex = 4;
            this.deleteSelectedButton.Text = "Delete selected songs";
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedSongsButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.discardButton.Location = new System.Drawing.Point(647, 24);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(63, 48);
            this.discardButton.TabIndex = 9;
            this.discardButton.Text = "Reset current";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadButton.Location = new System.Drawing.Point(716, 24);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(56, 48);
            this.uploadButton.TabIndex = 10;
            this.uploadButton.Text = "Upload current";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // metaDataLabel
            // 
            this.metaDataLabel.AutoSize = true;
            this.metaDataLabel.Location = new System.Drawing.Point(6, 90);
            this.metaDataLabel.Name = "metaDataLabel";
            this.metaDataLabel.Size = new System.Drawing.Size(232, 13);
            this.metaDataLabel.TabIndex = 11;
            this.metaDataLabel.Text = "2. Input file(s) to upload and its metadata below:";
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainGroupBox.Controls.Add(this.uploadLabel);
            this.mainGroupBox.Controls.Add(this.uploadButton);
            this.mainGroupBox.Controls.Add(this.metaDataLabel);
            this.mainGroupBox.Controls.Add(this.discardButton);
            this.mainGroupBox.Controls.Add(this.mediaPropertyGrid);
            this.mainGroupBox.Controls.Add(this.songsGroupBox);
            this.mainGroupBox.Controls.Add(this.label1);
            this.mainGroupBox.Controls.Add(this.comboBox1);
            this.mainGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(778, 481);
            this.mainGroupBox.TabIndex = 12;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Upload file and metadata";
            // 
            // uploadLabel
            // 
            this.uploadLabel.AutoSize = true;
            this.uploadLabel.Location = new System.Drawing.Point(644, 92);
            this.uploadLabel.Name = "uploadLabel";
            this.uploadLabel.Size = new System.Drawing.Size(100, 13);
            this.uploadLabel.TabIndex = 12;
            this.uploadLabel.Text = "Uploading file 1/2...";
            // 
            // UploadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 505);
            this.Controls.Add(this.mainGroupBox);
            this.Name = "UploadControl";
            this.Text = "Upload Media Control";
            this.songsGroupBox.ResumeLayout(false);
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid songPropertyGrid;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.ListView songListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PropertyGrid mediaPropertyGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox songsGroupBox;
        private System.Windows.Forms.Button discardButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button deleteAllItemsButton;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Label metaDataLabel;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.Label uploadLabel;
    }
}

