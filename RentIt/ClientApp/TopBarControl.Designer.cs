namespace ClientApp {
    partial class TopBarControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.HomeButton = new System.Windows.Forms.Button();
            this.MovieButton = new System.Windows.Forms.Button();
            this.MusicButton = new System.Windows.Forms.Button();
            this.BookButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.LogInLogOutButton = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SeparatorLine = new System.Windows.Forms.Label();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.UserNameLabel = new System.Windows.Forms.LinkLabel();
            this.creditsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(4, 4);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(55, 23);
            this.HomeButton.TabIndex = 0;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = true;
            this.HomeButton.Click += new System.EventHandler(this.HomeButtonClick);
            // 
            // MovieButton
            // 
            this.MovieButton.Location = new System.Drawing.Point(65, 4);
            this.MovieButton.Name = "MovieButton";
            this.MovieButton.Size = new System.Drawing.Size(55, 23);
            this.MovieButton.TabIndex = 1;
            this.MovieButton.Text = "Movies";
            this.MovieButton.UseVisualStyleBackColor = true;
            this.MovieButton.Click += new System.EventHandler(this.MovieButtonClick);
            // 
            // MusicButton
            // 
            this.MusicButton.Location = new System.Drawing.Point(126, 4);
            this.MusicButton.Name = "MusicButton";
            this.MusicButton.Size = new System.Drawing.Size(55, 23);
            this.MusicButton.TabIndex = 2;
            this.MusicButton.Text = "Music";
            this.MusicButton.UseVisualStyleBackColor = true;
            this.MusicButton.Click += new System.EventHandler(this.MusicButtonClick);
            // 
            // BookButton
            // 
            this.BookButton.Location = new System.Drawing.Point(187, 4);
            this.BookButton.Name = "BookButton";
            this.BookButton.Size = new System.Drawing.Size(55, 23);
            this.BookButton.TabIndex = 3;
            this.BookButton.Text = "Books";
            this.BookButton.UseVisualStyleBackColor = true;
            this.BookButton.Click += new System.EventHandler(this.BookButtonClick);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.TitleLabel.Location = new System.Drawing.Point(4, 34);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(71, 31);
            this.TitleLabel.TabIndex = 4;
            this.TitleLabel.Text = "Title";
            // 
            // LogInLogOutButton
            // 
            this.LogInLogOutButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogInLogOutButton.Location = new System.Drawing.Point(574, 4);
            this.LogInLogOutButton.Name = "LogInLogOutButton";
            this.LogInLogOutButton.Size = new System.Drawing.Size(91, 23);
            this.LogInLogOutButton.TabIndex = 5;
            this.LogInLogOutButton.Text = "Log in/register";
            this.LogInLogOutButton.UseVisualStyleBackColor = true;
            this.LogInLogOutButton.Click += new System.EventHandler(this.LogInLogOutButtonClick);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTextBox.Location = new System.Drawing.Point(392, 6);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 20);
            this.SearchTextBox.TabIndex = 6;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(498, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(44, 23);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "S";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButtonClick);
            // 
            // SeparatorLine
            // 
            this.SeparatorLine.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SeparatorLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLine.Location = new System.Drawing.Point(4, 68);
            this.SeparatorLine.Name = "SeparatorLine";
            this.SeparatorLine.Size = new System.Drawing.Size(656, 1);
            this.SeparatorLine.TabIndex = 8;
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Items.AddRange(new object[] {
            "All media",
            "Movies",
            "Music",
            "Books"});
            this.TypeComboBox.Location = new System.Drawing.Point(321, 5);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(65, 21);
            this.TypeComboBox.TabIndex = 9;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNameLabel.AutoEllipsis = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.UserNameLabel.Location = new System.Drawing.Point(501, 28);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(164, 18);
            this.UserNameLabel.TabIndex = 10;
            this.UserNameLabel.TabStop = true;
            this.UserNameLabel.Text = "user name";
            this.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.UserNameLabel.Visible = false;
            this.UserNameLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UserNameLabelLinkClicked);
            // 
            // creditsLabel
            // 
            this.creditsLabel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.creditsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.creditsLabel.Location = new System.Drawing.Point(498, 47);
            this.creditsLabel.Name = "creditsLabel";
            this.creditsLabel.Size = new System.Drawing.Size(167, 18);
            this.creditsLabel.TabIndex = 12;
            this.creditsLabel.Text = "(no credits)";
            this.creditsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.creditsLabel.Visible = false;
            // 
            // TopBarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.creditsLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.TypeComboBox);
            this.Controls.Add(this.SeparatorLine);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.LogInLogOutButton);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.BookButton);
            this.Controls.Add(this.MusicButton);
            this.Controls.Add(this.MovieButton);
            this.Controls.Add(this.HomeButton);
            this.MaximumSize = new System.Drawing.Size(99999, 76);
            this.MinimumSize = new System.Drawing.Size(604, 76);
            this.Name = "TopBarControl";
            this.Size = new System.Drawing.Size(668, 76);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HomeButton;
        private System.Windows.Forms.Button MovieButton;
        private System.Windows.Forms.Button MusicButton;
        private System.Windows.Forms.Button BookButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button LogInLogOutButton;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label SeparatorLine;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.LinkLabel UserNameLabel;
        private System.Windows.Forms.Label creditsLabel;
    }
}
