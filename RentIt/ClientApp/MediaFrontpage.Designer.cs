namespace ClientApp {
    partial class MediaFrontpage {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.grpNewHot = new System.Windows.Forms.GroupBox();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.lblNewPublisher = new System.Windows.Forms.Label();
            this.lblNewGenre = new System.Windows.Forms.Label();
            this.lblNewRelease = new System.Windows.Forms.Label();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.picNewThumb = new System.Windows.Forms.PictureBox();
            this.popularMediaGrid = new ClientApp.MediaGrid();
            this.genreList1 = new ClientApp.GenreList();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bestMediaGrid = new ClientApp.MediaGrid();
            this.newMediaGrid = new ClientApp.MediaGrid();
            this.grpNewHot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.picNewThumb)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNewHot
            // 
            this.grpNewHot.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNewHot.Controls.Add(this.lblNewPrice);
            this.grpNewHot.Controls.Add(this.lblNewPublisher);
            this.grpNewHot.Controls.Add(this.lblNewGenre);
            this.grpNewHot.Controls.Add(this.lblNewRelease);
            this.grpNewHot.Controls.Add(this.lblNewTitle);
            this.grpNewHot.Controls.Add(this.picNewThumb);
            this.grpNewHot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.grpNewHot.Location = new System.Drawing.Point(3, 3);
            this.grpNewHot.Name = "grpNewHot";
            this.grpNewHot.Size = new System.Drawing.Size(679, 205);
            this.grpNewHot.TabIndex = 0;
            this.grpNewHot.TabStop = false;
            this.grpNewHot.Text = "New and HOT";
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewPrice.AutoEllipsis = true;
            this.lblNewPrice.Location = new System.Drawing.Point(148, 165);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(289, 20);
            this.lblNewPrice.TabIndex = 5;
            this.lblNewPrice.Text = "price";
            // 
            // lblNewPublisher
            // 
            this.lblNewPublisher.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewPublisher.AutoEllipsis = true;
            this.lblNewPublisher.Location = new System.Drawing.Point(148, 105);
            this.lblNewPublisher.Name = "lblNewPublisher";
            this.lblNewPublisher.Size = new System.Drawing.Size(289, 20);
            this.lblNewPublisher.TabIndex = 4;
            this.lblNewPublisher.Text = "publisher";
            // 
            // lblNewGenre
            // 
            this.lblNewGenre.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewGenre.AutoEllipsis = true;
            this.lblNewGenre.Location = new System.Drawing.Point(148, 45);
            this.lblNewGenre.Name = "lblNewGenre";
            this.lblNewGenre.Size = new System.Drawing.Size(289, 20);
            this.lblNewGenre.TabIndex = 3;
            this.lblNewGenre.Text = "genre";
            // 
            // lblNewRelease
            // 
            this.lblNewRelease.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewRelease.AutoEllipsis = true;
            this.lblNewRelease.Location = new System.Drawing.Point(148, 65);
            this.lblNewRelease.Name = "lblNewRelease";
            this.lblNewRelease.Size = new System.Drawing.Size(289, 20);
            this.lblNewRelease.TabIndex = 2;
            this.lblNewRelease.Text = "release date";
            // 
            // lblNewTitle
            // 
            this.lblNewTitle.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewTitle.AutoEllipsis = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblNewTitle.Location = new System.Drawing.Point(148, 25);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(289, 20);
            this.lblNewTitle.TabIndex = 1;
            this.lblNewTitle.Text = "title";
            // 
            // picNewThumb
            // 
            this.picNewThumb.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picNewThumb.Location = new System.Drawing.Point(443, 25);
            this.picNewThumb.Name = "picNewThumb";
            this.picNewThumb.Size = new System.Drawing.Size(160, 160);
            this.picNewThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNewThumb.TabIndex = 0;
            this.picNewThumb.TabStop = false;
            // 
            // popularMediaGrid
            // 
            this.popularMediaGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popularMediaGrid.Location = new System.Drawing.Point(3, 3);
            this.popularMediaGrid.Name = "popularMediaGrid";
            this.popularMediaGrid.Size = new System.Drawing.Size(673, 99);
            this.popularMediaGrid.TabIndex = 0;
            // 
            // genreList1
            // 
            this.genreList1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genreList1.BackColor = System.Drawing.SystemColors.Control;
            this.genreList1.Location = new System.Drawing.Point(692, 14);
            this.genreList1.Name = "genreList1";
            this.genreList1.Size = new System.Drawing.Size(158, 517);
            this.genreList1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bestMediaGrid, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.newMediaGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.popularMediaGrid, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 214);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(679, 317);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // bestMediaGrid
            // 
            this.bestMediaGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bestMediaGrid.Location = new System.Drawing.Point(3, 213);
            this.bestMediaGrid.Name = "bestMediaGrid";
            this.bestMediaGrid.Size = new System.Drawing.Size(673, 101);
            this.bestMediaGrid.TabIndex = 2;
            // 
            // newMediaGrid
            // 
            this.newMediaGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newMediaGrid.Location = new System.Drawing.Point(3, 108);
            this.newMediaGrid.Name = "newMediaGrid";
            this.newMediaGrid.Size = new System.Drawing.Size(673, 99);
            this.newMediaGrid.TabIndex = 1;
            // 
            // MediaFrontpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.genreList1);
            this.Controls.Add(this.grpNewHot);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MediaFrontpage";
            this.Size = new System.Drawing.Size(853, 534);
            this.grpNewHot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.picNewThumb)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNewHot;
        private System.Windows.Forms.Label lblNewRelease;
        private System.Windows.Forms.Label lblNewTitle;
        private System.Windows.Forms.PictureBox picNewThumb;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Label lblNewPublisher;
        private System.Windows.Forms.Label lblNewGenre;
        private MediaGrid popularMediaGrid;
        private GenreList genreList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MediaGrid bestMediaGrid;
        private MediaGrid newMediaGrid;
    }
}
