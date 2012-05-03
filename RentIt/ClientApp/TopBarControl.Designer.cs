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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AccountButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SeparatorLine = new System.Windows.Forms.Label();
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
            // 
            // MovieButton
            // 
            this.MovieButton.Location = new System.Drawing.Point(65, 4);
            this.MovieButton.Name = "MovieButton";
            this.MovieButton.Size = new System.Drawing.Size(55, 23);
            this.MovieButton.TabIndex = 1;
            this.MovieButton.Text = "Movies";
            this.MovieButton.UseVisualStyleBackColor = true;
            // 
            // MusicButton
            // 
            this.MusicButton.Location = new System.Drawing.Point(126, 4);
            this.MusicButton.Name = "MusicButton";
            this.MusicButton.Size = new System.Drawing.Size(55, 23);
            this.MusicButton.TabIndex = 2;
            this.MusicButton.Text = "Music";
            this.MusicButton.UseVisualStyleBackColor = true;
            // 
            // BookButton
            // 
            this.BookButton.Location = new System.Drawing.Point(187, 4);
            this.BookButton.Name = "BookButton";
            this.BookButton.Size = new System.Drawing.Size(55, 23);
            this.BookButton.TabIndex = 3;
            this.BookButton.Text = "Books";
            this.BookButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(372, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // AccountButton
            // 
            this.AccountButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountButton.Location = new System.Drawing.Point(538, 4);
            this.AccountButton.Name = "AccountButton";
            this.AccountButton.Size = new System.Drawing.Size(75, 23);
            this.AccountButton.TabIndex = 5;
            this.AccountButton.Text = "Account";
            this.AccountButton.UseVisualStyleBackColor = true;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(478, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(41, 23);
            this.SearchButton.TabIndex = 6;
            this.SearchButton.Text = "S";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.TitleLabel.Location = new System.Drawing.Point(3, 30);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(71, 31);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "Title";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(245, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // SeparatorLine
            // 
            this.SeparatorLine.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SeparatorLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLine.Location = new System.Drawing.Point(4, 64);
            this.SeparatorLine.Name = "SeparatorLine";
            this.SeparatorLine.Size = new System.Drawing.Size(608, 1);
            this.SeparatorLine.TabIndex = 9;
            // 
            // TopBarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SeparatorLine);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.AccountButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BookButton);
            this.Controls.Add(this.MusicButton);
            this.Controls.Add(this.MovieButton);
            this.Controls.Add(this.HomeButton);
            this.Name = "TopBarControl";
            this.Size = new System.Drawing.Size(616, 71);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HomeButton;
        private System.Windows.Forms.Button MovieButton;
        private System.Windows.Forms.Button MusicButton;
        private System.Windows.Forms.Button BookButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button AccountButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label SeparatorLine;
    }
}
