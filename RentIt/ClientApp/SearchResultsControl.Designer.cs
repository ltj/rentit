namespace ClientApp {
    partial class SearchResultsControl {
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
            this.GenreFilter = new System.Windows.Forms.ListBox();
            this.TypeFilter = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filterButton = new System.Windows.Forms.Button();
            this.priceFilter = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.results = new ClientApp.DetailedMediaListControl();
            this.SuspendLayout();
            // 
            // GenreFilter
            // 
            this.GenreFilter.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GenreFilter.FormattingEnabled = true;
            this.GenreFilter.IntegralHeight = false;
            this.GenreFilter.Location = new System.Drawing.Point(5, 139);
            this.GenreFilter.Name = "GenreFilter";
            this.GenreFilter.Size = new System.Drawing.Size(120, 160);
            this.GenreFilter.TabIndex = 15;
            this.GenreFilter.SelectedIndexChanged += new System.EventHandler(this.GenreFilterSelectedIndexChanged);
            // 
            // TypeFilter
            // 
            this.TypeFilter.FormattingEnabled = true;
            this.TypeFilter.Items.AddRange(new object[] {
            "All",
            "Movies",
            "Music",
            "Books"});
            this.TypeFilter.Location = new System.Drawing.Point(4, 48);
            this.TypeFilter.Name = "TypeFilter";
            this.TypeFilter.Size = new System.Drawing.Size(120, 69);
            this.TypeFilter.TabIndex = 14;
            this.TypeFilter.SelectedIndexChanged += new System.EventHandler(this.TypeFilterSelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Genre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type";
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.resultsLabel.Location = new System.Drawing.Point(138, 7);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(60, 16);
            this.resultsLabel.TabIndex = 11;
            this.resultsLabel.Text = "Results";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Filters";
            // 
            // filterButton
            // 
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterButton.Location = new System.Drawing.Point(4, 424);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(121, 23);
            this.filterButton.TabIndex = 17;
            this.filterButton.Text = "Filter results";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButtonClick);
            // 
            // priceFilter
            // 
            this.priceFilter.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.priceFilter.FormattingEnabled = true;
            this.priceFilter.Location = new System.Drawing.Point(5, 322);
            this.priceFilter.Name = "priceFilter";
            this.priceFilter.Size = new System.Drawing.Size(120, 95);
            this.priceFilter.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Price";
            // 
            // results
            // 
            this.results.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.results.Location = new System.Drawing.Point(131, 13);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(606, 436);
            this.results.TabIndex = 16;
            // 
            // SearchResultsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.priceFilter);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.results);
            this.Controls.Add(this.GenreFilter);
            this.Controls.Add(this.TypeFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "SearchResultsControl";
            this.Size = new System.Drawing.Size(740, 452);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox GenreFilter;
        private System.Windows.Forms.ListBox TypeFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.Label label1;
        private DetailedMediaListControl results;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.ListBox priceFilter;
        private System.Windows.Forms.Label label2;
    }
}
