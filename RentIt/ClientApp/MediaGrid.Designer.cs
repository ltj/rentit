namespace ClientApp
{
    partial class MediaGrid
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
            this.ListViewGrid = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.titleValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListViewGrid
            // 
            this.ListViewGrid.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ListViewGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ListViewGrid.Location = new System.Drawing.Point(3, 23);
            this.ListViewGrid.MultiSelect = false;
            this.ListViewGrid.Name = "ListViewGrid";
            this.ListViewGrid.Size = new System.Drawing.Size(455, 93);
            this.ListViewGrid.TabIndex = 0;
            this.ListViewGrid.TileSize = new System.Drawing.Size(168, 80);
            this.ListViewGrid.UseCompatibleStateImageBehavior = false;
            this.ListViewGrid.View = System.Windows.Forms.View.Tile;
            // 
            // titleValueLabel
            // 
            this.titleValueLabel.AutoSize = true;
            this.titleValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.titleValueLabel.Location = new System.Drawing.Point(4, 0);
            this.titleValueLabel.Name = "titleValueLabel";
            this.titleValueLabel.Size = new System.Drawing.Size(89, 20);
            this.titleValueLabel.TabIndex = 1;
            this.titleValueLabel.Text = "TitleValue";
            // 
            // MediaGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.titleValueLabel);
            this.Controls.Add(this.ListViewGrid);
            this.Name = "MediaGrid";
            this.Size = new System.Drawing.Size(462, 119);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListViewGrid;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label titleValueLabel;
    }
}
