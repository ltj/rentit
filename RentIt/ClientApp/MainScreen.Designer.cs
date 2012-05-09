﻿namespace ClientApp {
    partial class MainScreen {
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
            this.moviesButton = new System.Windows.Forms.Button();
            this.booksButton = new System.Windows.Forms.Button();
            this.musicButton = new System.Windows.Forms.Button();
            this.musicList = new ClientApp.ListOfMedia();
            this.booksList = new ClientApp.ListOfMedia();
            this.moviesList = new ClientApp.ListOfMedia();
            this.SuspendLayout();
            // 
            // moviesButton
            // 
            this.moviesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.moviesButton.Location = new System.Drawing.Point(3, 3);
            this.moviesButton.Name = "moviesButton";
            this.moviesButton.Size = new System.Drawing.Size(218, 82);
            this.moviesButton.TabIndex = 0;
            this.moviesButton.Text = "Movies";
            this.moviesButton.UseVisualStyleBackColor = true;
            // 
            // booksButton
            // 
            this.booksButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.booksButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.booksButton.Location = new System.Drawing.Point(228, 3);
            this.booksButton.Name = "booksButton";
            this.booksButton.Size = new System.Drawing.Size(218, 82);
            this.booksButton.TabIndex = 1;
            this.booksButton.Text = "Books";
            this.booksButton.UseVisualStyleBackColor = true;
            // 
            // musicButton
            // 
            this.musicButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.musicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.musicButton.Location = new System.Drawing.Point(453, 3);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(218, 82);
            this.musicButton.TabIndex = 2;
            this.musicButton.Text = "Music";
            this.musicButton.UseVisualStyleBackColor = true;
            // 
            // musicList
            // 
            this.musicList.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.musicList.Location = new System.Drawing.Point(453, 92);
            this.musicList.Name = "musicList";
            this.musicList.Size = new System.Drawing.Size(218, 361);
            this.musicList.TabIndex = 5;
            // 
            // booksList
            // 
            this.booksList.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.booksList.Location = new System.Drawing.Point(228, 92);
            this.booksList.Name = "booksList";
            this.booksList.Size = new System.Drawing.Size(218, 361);
            this.booksList.TabIndex = 4;
            // 
            // moviesList
            // 
            this.moviesList.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.moviesList.Location = new System.Drawing.Point(4, 92);
            this.moviesList.Name = "moviesList";
            this.moviesList.Size = new System.Drawing.Size(217, 361);
            this.moviesList.TabIndex = 3;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.musicList);
            this.Controls.Add(this.booksList);
            this.Controls.Add(this.moviesList);
            this.Controls.Add(this.musicButton);
            this.Controls.Add(this.booksButton);
            this.Controls.Add(this.moviesButton);
            this.MinimumSize = new System.Drawing.Size(676, 310);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(676, 456);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button moviesButton;
        private System.Windows.Forms.Button booksButton;
        private System.Windows.Forms.Button musicButton;
        private ListOfMedia moviesList;
        private ListOfMedia booksList;
        private ListOfMedia musicList;
    }
}