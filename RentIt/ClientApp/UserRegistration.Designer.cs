namespace ClientApp {
    partial class UserRegistration {
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
			this.components = new System.ComponentModel.Container();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtFullName = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblUserName = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblPasswordConfim = new System.Windows.Forms.Label();
			this.lblFullName = new System.Windows.Forms.Label();
			this.lblEmail = new System.Windows.Forms.Label();
			this.btnRegister = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtPasswordConfirm = new System.Windows.Forms.TextBox();
			this.errUserName = new System.Windows.Forms.ErrorProvider(this.components);
			this.errPassword = new System.Windows.Forms.ErrorProvider(this.components);
			this.errFullName = new System.Windows.Forms.ErrorProvider(this.components);
			this.errEmail = new System.Windows.Forms.ErrorProvider(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.errUserName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errFullName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errEmail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(108, 88);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(213, 20);
			this.txtUserName.TabIndex = 0;
			this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
			this.txtUserName.Validated += new System.EventHandler(this.txtUserName_Validated);
			// 
			// txtFullName
			// 
			this.txtFullName.Location = new System.Drawing.Point(108, 166);
			this.txtFullName.Name = "txtFullName";
			this.txtFullName.Size = new System.Drawing.Size(213, 20);
			this.txtFullName.TabIndex = 3;
			this.txtFullName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFullName_Validating);
			this.txtFullName.Validated += new System.EventHandler(this.txtFullName_Validated);
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(108, 192);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(213, 20);
			this.txtEmail.TabIndex = 4;
			this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
			this.txtEmail.Validated += new System.EventHandler(this.txtEmail_Validated);
			// 
			// lblUserName
			// 
			this.lblUserName.AutoSize = true;
			this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblUserName.Location = new System.Drawing.Point(41, 88);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(61, 13);
			this.lblUserName.TabIndex = 5;
			this.lblUserName.Text = "User name:";
			this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPassword.Location = new System.Drawing.Point(46, 114);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password:";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPasswordConfim
			// 
			this.lblPasswordConfim.AutoSize = true;
			this.lblPasswordConfim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPasswordConfim.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPasswordConfim.Location = new System.Drawing.Point(9, 140);
			this.lblPasswordConfim.Name = "lblPasswordConfim";
			this.lblPasswordConfim.Size = new System.Drawing.Size(93, 13);
			this.lblPasswordConfim.TabIndex = 7;
			this.lblPasswordConfim.Text = "Confirm password:";
			this.lblPasswordConfim.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblFullName
			// 
			this.lblFullName.AutoSize = true;
			this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFullName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFullName.Location = new System.Drawing.Point(47, 166);
			this.lblFullName.Name = "lblFullName";
			this.lblFullName.Size = new System.Drawing.Size(55, 13);
			this.lblFullName.TabIndex = 8;
			this.lblFullName.Text = "Full name:";
			this.lblFullName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblEmail.Location = new System.Drawing.Point(67, 192);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(35, 13);
			this.lblEmail.TabIndex = 9;
			this.lblEmail.Text = "Email:";
			this.lblEmail.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnRegister
			// 
			this.btnRegister.Location = new System.Drawing.Point(108, 220);
			this.btnRegister.Name = "btnRegister";
			this.btnRegister.Size = new System.Drawing.Size(75, 23);
			this.btnRegister.TabIndex = 5;
			this.btnRegister.Text = "Register";
			this.btnRegister.UseVisualStyleBackColor = true;
			this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(108, 114);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(213, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
			this.txtPassword.Validated += new System.EventHandler(this.txtPassword_Validated);
			// 
			// txtPasswordConfirm
			// 
			this.txtPasswordConfirm.Location = new System.Drawing.Point(108, 140);
			this.txtPasswordConfirm.Name = "txtPasswordConfirm";
			this.txtPasswordConfirm.Size = new System.Drawing.Size(213, 20);
			this.txtPasswordConfirm.TabIndex = 2;
			this.txtPasswordConfirm.UseSystemPasswordChar = true;
			this.txtPasswordConfirm.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasswordConfirm_Validating);
			this.txtPasswordConfirm.Validated += new System.EventHandler(this.txtPasswordConfirm_Validated);
			// 
			// errUserName
			// 
			this.errUserName.ContainerControl = this;
			// 
			// errPassword
			// 
			this.errPassword.ContainerControl = this;
			// 
			// errFullName
			// 
			this.errFullName.ContainerControl = this;
			// 
			// errEmail
			// 
			this.errEmail.ContainerControl = this;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(192, 220);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "User name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Password:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(69, 138);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 13;
			this.button1.Text = "Log In";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(69, 88);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(213, 20);
			this.textBox1.TabIndex = 14;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(69, 114);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(213, 20);
			this.textBox2.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(42, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(203, 31);
			this.label3.TabIndex = 16;
			this.label3.Text = "Already a user?";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(66, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(161, 13);
			this.label4.TabIndex = 17;
			this.label4.Text = "Provide your log-in details below.";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(67, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(200, 31);
			this.label5.TabIndex = 18;
			this.label5.Text = "Not registered?";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(105, 66);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(202, 13);
			this.label6.TabIndex = 19;
			this.label6.Text = "Submit your preferred log-in details below.";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.button1);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.textBox2);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.textBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label6);
			this.splitContainer1.Panel2.Controls.Add(this.txtFullName);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.txtUserName);
			this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
			this.splitContainer1.Panel2.Controls.Add(this.txtEmail);
			this.splitContainer1.Panel2.Controls.Add(this.txtPasswordConfirm);
			this.splitContainer1.Panel2.Controls.Add(this.lblUserName);
			this.splitContainer1.Panel2.Controls.Add(this.txtPassword);
			this.splitContainer1.Panel2.Controls.Add(this.lblPassword);
			this.splitContainer1.Panel2.Controls.Add(this.btnRegister);
			this.splitContainer1.Panel2.Controls.Add(this.lblPasswordConfim);
			this.splitContainer1.Panel2.Controls.Add(this.lblEmail);
			this.splitContainer1.Panel2.Controls.Add(this.lblFullName);
			this.splitContainer1.Size = new System.Drawing.Size(670, 262);
			this.splitContainer1.SplitterDistance = 305;
			this.splitContainer1.TabIndex = 20;
			// 
			// UserRegistration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.splitContainer1);
			this.Name = "UserRegistration";
			this.Size = new System.Drawing.Size(680, 262);
			((System.ComponentModel.ISupportInitialize)(this.errUserName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errFullName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errEmail)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblPasswordConfim;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPasswordConfirm;
        private System.Windows.Forms.ErrorProvider errUserName;
        private System.Windows.Forms.ErrorProvider errPassword;
        private System.Windows.Forms.ErrorProvider errFullName;
        private System.Windows.Forms.ErrorProvider errEmail;
        private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;

    }
}
