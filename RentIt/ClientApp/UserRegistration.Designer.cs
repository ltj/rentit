namespace ClientApp
{
    partial class UserRegistration
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loginUserNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPasswordConfirm = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblPasswordConfim = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.errUserName = new System.Windows.Forms.ErrorProvider(this.components);
            this.errPassword = new System.Windows.Forms.ErrorProvider(this.components);
            this.errFullName = new System.Windows.Forms.ErrorProvider(this.components);
            this.errEmail = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.errUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.errPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.errFullName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.errEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.loginPasswordTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.loginUserNameTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtFullName);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtUserName);
            this.splitContainer1.Panel2.Controls.Add(this.txtEmail);
            this.splitContainer1.Panel2.Controls.Add(this.txtPasswordConfirm);
            this.splitContainer1.Panel2.Controls.Add(this.lblUserName);
            this.splitContainer1.Panel2.Controls.Add(this.txtPassword);
            this.splitContainer1.Panel2.Controls.Add(this.lblPassword);
            this.splitContainer1.Panel2.Controls.Add(this.btnRegister);
            this.splitContainer1.Panel2.Controls.Add(this.lblPasswordConfim);
            this.splitContainer1.Panel2.Controls.Add(this.lblEmail);
            this.splitContainer1.Panel2.Controls.Add(this.lblFullName);
            this.splitContainer1.Size = new System.Drawing.Size(759, 366);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(65, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 31);
            this.label3.TabIndex = 16;
            this.label3.Text = "Already a user?";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(126, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Log In";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Provide your log-in details below.";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "User name:";
            // 
            // loginPasswordTextBox
            // 
            this.loginPasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginPasswordTextBox.Location = new System.Drawing.Point(92, 141);
            this.loginPasswordTextBox.Name = "loginPasswordTextBox";
            this.loginPasswordTextBox.Size = new System.Drawing.Size(213, 20);
            this.loginPasswordTextBox.TabIndex = 15;
            this.loginPasswordTextBox.UseSystemPasswordChar = true;
            this.loginPasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginKeyPressed);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password:";
            // 
            // loginUserNameTextBox
            // 
            this.loginUserNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginUserNameTextBox.Location = new System.Drawing.Point(92, 115);
            this.loginUserNameTextBox.Name = "loginUserNameTextBox";
            this.loginUserNameTextBox.Size = new System.Drawing.Size(213, 20);
            this.loginUserNameTextBox.TabIndex = 14;
            this.loginUserNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginKeyPressed);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Submit your preferred log-in details below.";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFullName.Location = new System.Drawing.Point(141, 193);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(213, 20);
            this.txtFullName.TabIndex = 3;
            this.txtFullName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFullName_Validating);
            this.txtFullName.Validated += new System.EventHandler(this.txtFullName_Validated);
            this.txtFullName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterUserKeyPressed);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label5.Location = new System.Drawing.Point(112, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 31);
            this.label5.TabIndex = 18;
            this.label5.Text = "Not registered?";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserName.Location = new System.Drawing.Point(141, 115);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(213, 20);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            this.txtUserName.Validated += new System.EventHandler(this.txtUserName_Validated);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterUserKeyPressed);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmail.Location = new System.Drawing.Point(141, 219);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(213, 20);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            this.txtEmail.Validated += new System.EventHandler(this.txtEmail_Validated);
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterUserKeyPressed);
            // 
            // txtPasswordConfirm
            // 
            this.txtPasswordConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPasswordConfirm.Location = new System.Drawing.Point(141, 167);
            this.txtPasswordConfirm.Name = "txtPasswordConfirm";
            this.txtPasswordConfirm.Size = new System.Drawing.Size(213, 20);
            this.txtPasswordConfirm.TabIndex = 2;
            this.txtPasswordConfirm.UseSystemPasswordChar = true;
            this.txtPasswordConfirm.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasswordConfirm_Validating);
            this.txtPasswordConfirm.Validated += new System.EventHandler(this.txtPasswordConfirm_Validated);
            this.txtPasswordConfirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterUserKeyPressed);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserName.Location = new System.Drawing.Point(74, 115);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 13);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "User name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPassword.Location = new System.Drawing.Point(141, 141);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            this.txtPassword.Validated += new System.EventHandler(this.txtPassword_Validated);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterUserKeyPressed);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPassword.Location = new System.Drawing.Point(79, 141);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegister.Location = new System.Drawing.Point(193, 245);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblPasswordConfim
            // 
            this.lblPasswordConfim.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPasswordConfim.AutoSize = true;
            this.lblPasswordConfim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblPasswordConfim.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPasswordConfim.Location = new System.Drawing.Point(42, 167);
            this.lblPasswordConfim.Name = "lblPasswordConfim";
            this.lblPasswordConfim.Size = new System.Drawing.Size(93, 13);
            this.lblPasswordConfim.TabIndex = 7;
            this.lblPasswordConfim.Text = "Confirm password:";
            this.lblPasswordConfim.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail.Location = new System.Drawing.Point(100, 219);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFullName
            // 
            this.lblFullName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblFullName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFullName.Location = new System.Drawing.Point(80, 193);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(55, 13);
            this.lblFullName.TabIndex = 8;
            this.lblFullName.Text = "Full name:";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // UserRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UserRegistration";
            this.Size = new System.Drawing.Size(765, 372);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.errUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.errPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.errFullName)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.errEmail)).EndInit();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox loginPasswordTextBox;
        private System.Windows.Forms.TextBox loginUserNameTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;

    }
}
