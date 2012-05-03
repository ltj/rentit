namespace ClientApp
{
    partial class MediaSideBar
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
            this.thumbnailBox = new System.Windows.Forms.PictureBox();
            this.rentButton = new System.Windows.Forms.Button();
            this.priceLabel = new System.Windows.Forms.Label();
            this.mediaDetailLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailBox)).BeginInit();
            this.SuspendLayout();
            // 
            // thumbnailBox
            // 
            this.thumbnailBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailBox.Location = new System.Drawing.Point(4, 4);
            this.thumbnailBox.Name = "thumbnailBox";
            this.thumbnailBox.Size = new System.Drawing.Size(217, 214);
            this.thumbnailBox.TabIndex = 1;
            this.thumbnailBox.TabStop = false;
            // 
            // rentButton
            // 
            this.rentButton.Location = new System.Drawing.Point(7, 248);
            this.rentButton.Name = "rentButton";
            this.rentButton.Size = new System.Drawing.Size(88, 37);
            this.rentButton.TabIndex = 2;
            this.rentButton.Text = "Rent Media";
            this.rentButton.UseVisualStyleBackColor = true;
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(4, 232);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(56, 13);
            this.priceLabel.TabIndex = 3;
            this.priceLabel.Text = "priceLabel";
            // 
            // mediaDetailLabel
            // 
            this.mediaDetailLabel.AutoSize = true;
            this.mediaDetailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediaDetailLabel.Location = new System.Drawing.Point(4, 307);
            this.mediaDetailLabel.Name = "mediaDetailLabel";
            this.mediaDetailLabel.Size = new System.Drawing.Size(82, 13);
            this.mediaDetailLabel.TabIndex = 4;
            this.mediaDetailLabel.Text = "Media details";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 324);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 160);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // MediaSideBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mediaDetailLabel);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.rentButton);
            this.Controls.Add(this.thumbnailBox);
            this.Name = "MediaSideBar";
            this.Size = new System.Drawing.Size(224, 511);
            this.Load += new System.EventHandler(this.MediaSideBar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox thumbnailBox;
        private System.Windows.Forms.Button rentButton;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label mediaDetailLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
