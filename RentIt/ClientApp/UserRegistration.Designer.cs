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
            ((System.ComponentModel.ISupportInitialize)(this.errUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errFullName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(113, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(213, 20);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            this.txtUserName.Validated += new System.EventHandler(this.txtUserName_Validated);
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(113, 81);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(213, 20);
            this.txtFullName.TabIndex = 3;
            this.txtFullName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFullName_Validating);
            this.txtFullName.Validated += new System.EventHandler(this.txtFullName_Validated);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(113, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(213, 20);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            this.txtEmail.Validated += new System.EventHandler(this.txtEmail_Validated);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUserName.Location = new System.Drawing.Point(36, 6);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(71, 13);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "User name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPassword.Location = new System.Drawing.Point(42, 32);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(65, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPasswordConfim
            // 
            this.lblPasswordConfim.AutoSize = true;
            this.lblPasswordConfim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordConfim.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPasswordConfim.Location = new System.Drawing.Point(-3, 58);
            this.lblPasswordConfim.Name = "lblPasswordConfim";
            this.lblPasswordConfim.Size = new System.Drawing.Size(110, 13);
            this.lblPasswordConfim.TabIndex = 7;
            this.lblPasswordConfim.Text = "Confirm password:";
            this.lblPasswordConfim.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblFullName.Location = new System.Drawing.Point(42, 84);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(65, 13);
            this.lblFullName.TabIndex = 8;
            this.lblFullName.Text = "Full name:";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblEmail.Location = new System.Drawing.Point(66, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 13);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRegister
            // 
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(113, 138);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(113, 29);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            this.txtPassword.Validated += new System.EventHandler(this.txtPassword_Validated);
            // 
            // txtPasswordConfirm
            // 
            this.txtPasswordConfirm.Location = new System.Drawing.Point(113, 55);
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
            // UserRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.txtPasswordConfirm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblPasswordConfim);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtUserName);
            this.Name = "UserRegistration";
            this.Size = new System.Drawing.Size(355, 168);
            ((System.ComponentModel.ISupportInitialize)(this.errUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errFullName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errEmail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

    }
}
