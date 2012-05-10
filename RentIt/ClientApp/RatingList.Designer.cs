namespace ClientApp {
    partial class RatingList {
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
            this.ReviewText = new System.Windows.Forms.TextBox();
            this.RatingSelector = new System.Windows.Forms.ComboBox();
            this.SubmitReviewButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AvgRating = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AvgRatingCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reviewList = new ClientApp.PagedRatingsListControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReviewText
            // 
            this.ReviewText.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReviewText.Location = new System.Drawing.Point(6, 19);
            this.ReviewText.Multiline = true;
            this.ReviewText.Name = "ReviewText";
            this.ReviewText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReviewText.Size = new System.Drawing.Size(418, 75);
            this.ReviewText.TabIndex = 1;
            // 
            // RatingSelector
            // 
            this.RatingSelector.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RatingSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RatingSelector.FormattingEnabled = true;
            this.RatingSelector.Items.AddRange(new object[] {
            "1 (One) - Worst",
            "2 (Two)",
            "3 (Three)",
            "4 (Four)",
            "5 (Five) - Best"});
            this.RatingSelector.Location = new System.Drawing.Point(430, 19);
            this.RatingSelector.Name = "RatingSelector";
            this.RatingSelector.Size = new System.Drawing.Size(119, 21);
            this.RatingSelector.TabIndex = 2;
            // 
            // SubmitReviewButton
            // 
            this.SubmitReviewButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitReviewButton.Location = new System.Drawing.Point(430, 46);
            this.SubmitReviewButton.Name = "SubmitReviewButton";
            this.SubmitReviewButton.Size = new System.Drawing.Size(119, 48);
            this.SubmitReviewButton.TabIndex = 3;
            this.SubmitReviewButton.Text = "Submit review";
            this.SubmitReviewButton.UseVisualStyleBackColor = true;
            this.SubmitReviewButton.Click += new System.EventHandler(this.SubmitReviewButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Average rating:";
            // 
            // AvgRating
            // 
            this.AvgRating.AutoSize = true;
            this.AvgRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.AvgRating.Location = new System.Drawing.Point(125, 4);
            this.AvgRating.Name = "AvgRating";
            this.AvgRating.Size = new System.Drawing.Size(19, 20);
            this.AvgRating.TabIndex = 6;
            this.AvgRating.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.Location = new System.Drawing.Point(3, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "What other users are saying";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ReviewText);
            this.groupBox1.Controls.Add(this.RatingSelector);
            this.groupBox1.Controls.Add(this.SubmitReviewButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 101);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Review this media";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(150, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "based on";
            // 
            // AvgRatingCount
            // 
            this.AvgRatingCount.AutoSize = true;
            this.AvgRatingCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.AvgRatingCount.Location = new System.Drawing.Point(231, 4);
            this.AvgRatingCount.Name = "AvgRatingCount";
            this.AvgRatingCount.Size = new System.Drawing.Size(19, 20);
            this.AvgRatingCount.TabIndex = 10;
            this.AvgRatingCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label4.Location = new System.Drawing.Point(256, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "ratings";
            // 
            // reviewList
            // 
            this.reviewList.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reviewList.Location = new System.Drawing.Point(4, 157);
            this.reviewList.Name = "reviewList";
            this.reviewList.Size = new System.Drawing.Size(554, 225);
            this.reviewList.TabIndex = 12;
            // 
            // RatingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reviewList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AvgRatingCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AvgRating);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "RatingList";
            this.Size = new System.Drawing.Size(561, 385);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ReviewText;
        private System.Windows.Forms.ComboBox RatingSelector;
        private System.Windows.Forms.Button SubmitReviewButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AvgRating;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AvgRatingCount;
        private System.Windows.Forms.Label label4;
        private PagedRatingsListControl reviewList;
    }
}
