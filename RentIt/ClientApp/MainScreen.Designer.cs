namespace ClientApp {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // moviesButton
            // 
            this.moviesButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.moviesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.moviesButton.Image = global::ClientApp.Properties.Resources.movie;
            this.moviesButton.Location = new System.Drawing.Point(3, 3);
            this.moviesButton.Name = "moviesButton";
            this.moviesButton.Size = new System.Drawing.Size(216, 82);
            this.moviesButton.TabIndex = 0;
            this.moviesButton.Text = "Movies";
            this.moviesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.moviesButton.UseVisualStyleBackColor = true;
            this.moviesButton.Click += new System.EventHandler(this.MoviesButtonClick);
            // 
            // booksButton
            // 
            this.booksButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.booksButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.booksButton.Image = global::ClientApp.Properties.Resources.book;
            this.booksButton.Location = new System.Drawing.Point(225, 3);
            this.booksButton.Name = "booksButton";
            this.booksButton.Size = new System.Drawing.Size(216, 82);
            this.booksButton.TabIndex = 1;
            this.booksButton.Text = "Books";
            this.booksButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.booksButton.UseVisualStyleBackColor = true;
            this.booksButton.Click += new System.EventHandler(this.BooksButtonClick);
            // 
            // musicButton
            // 
            this.musicButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.musicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.musicButton.Image = global::ClientApp.Properties.Resources.music;
            this.musicButton.Location = new System.Drawing.Point(447, 3);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(217, 82);
            this.musicButton.TabIndex = 2;
            this.musicButton.Text = "Music";
            this.musicButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.musicButton.UseVisualStyleBackColor = true;
            this.musicButton.Click += new System.EventHandler(this.MusicButtonClick);
            // 
            // musicList
            // 
            this.musicList.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.musicList.Location = new System.Drawing.Point(447, 91);
            this.musicList.Name = "musicList";
            this.musicList.Size = new System.Drawing.Size(217, 358);
            this.musicList.TabIndex = 5;
            // 
            // booksList
            // 
            this.booksList.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.booksList.Location = new System.Drawing.Point(225, 91);
            this.booksList.Name = "booksList";
            this.booksList.Size = new System.Drawing.Size(216, 358);
            this.booksList.TabIndex = 4;
            // 
            // moviesList
            // 
            this.moviesList.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.moviesList.Location = new System.Drawing.Point(3, 91);
            this.moviesList.Name = "moviesList";
            this.moviesList.Size = new System.Drawing.Size(216, 358);
            this.moviesList.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.musicList, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.moviesButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.musicButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.booksList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.moviesList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.booksButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(667, 452);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(676, 310);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(676, 456);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button moviesButton;
        private System.Windows.Forms.Button booksButton;
        private System.Windows.Forms.Button musicButton;
        private ListOfMedia moviesList;
        private ListOfMedia booksList;
        private ListOfMedia musicList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
