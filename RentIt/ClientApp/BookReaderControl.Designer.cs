namespace ClientApp {
    partial class BookReaderControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookReaderControl));
            this.titleLabel = new System.Windows.Forms.Label();
            this.pdfReader = new AxAcroPDFLib.AxAcroPDF();
            this.viewBookDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pdfReader)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(4, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(108, 25);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Book Title";
            // 
            // pdfReader
            // 
            this.pdfReader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfReader.Enabled = true;
            this.pdfReader.Location = new System.Drawing.Point(0, 61);
            this.pdfReader.Name = "pdfReader";
            this.pdfReader.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfReader.OcxState")));
            this.pdfReader.Size = new System.Drawing.Size(657, 389);
            this.pdfReader.TabIndex = 3;
            // 
            // viewBookDetails
            // 
            this.viewBookDetails.Location = new System.Drawing.Point(9, 32);
            this.viewBookDetails.Name = "viewBookDetails";
            this.viewBookDetails.Size = new System.Drawing.Size(75, 23);
            this.viewBookDetails.TabIndex = 4;
            this.viewBookDetails.Text = "Book details";
            this.viewBookDetails.UseVisualStyleBackColor = true;
            // 
            // BookReaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewBookDetails);
            this.Controls.Add(this.pdfReader);
            this.Controls.Add(this.titleLabel);
            this.Name = "BookReaderControl";
            this.Size = new System.Drawing.Size(657, 450);
            ((System.ComponentModel.ISupportInitialize)(this.pdfReader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private AxAcroPDFLib.AxAcroPDF pdfReader;
        private System.Windows.Forms.Button viewBookDetails;
    }
}
