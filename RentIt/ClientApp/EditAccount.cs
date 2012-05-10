using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.Security.Cryptography;

namespace ClientApp {
    public partial class EditAccount : UserControl {

        private RentItClient rentIt;
        private RentIt.Account account;
        private RentIt.AccountCredentials credentials;

        public EditAccount() {
            InitializeComponent();
        }

        internal RentItClient Proxy {
            set { this.rentIt = value; }
        }

        internal RentIt.AccountCredentials Credentials {
            set { 
                this.credentials = value;
                setFields();
            }
        }

        private void txtPasswordConfirm_Validating(object sender, CancelEventArgs e) {
            if (txtPasswordConfirm.Text != txtPassword.Text && errPassword.GetError(txtPassword) == "") {
                e.Cancel = true;
                txtPassword.Select(0, txtPassword.Text.Length);

                errPassword.SetError(txtPassword, "Passwords do not match");
            }
        }

        private void txtPasswordConfirm_Validated(object sender, EventArgs e) {
            errPassword.SetError(txtPassword, "");
        }

        // validator function for Full name field
        private bool ValidateFullName(string name, out string errorMessage) {
            if (name == "") {
                errorMessage = "Please enter your name";
                return false;
            }

            if (Regex.IsMatch(name, @"^[\w\s]+?$")) {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please only use alphanumerics and spaces";
            return false;
        }

        // Email validation helper method
        private bool ValidateEmail(string mail, out string errorMessage) {
            if (mail == "") {
                errorMessage = "Please enter an email address";
                return false;
            }

            if (Regex.IsMatch(mail, @"^[\w\d\.-_]+@([\w\d-_]+?\.)+\w{2,4}$")) {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please enter a valid email address";
            return false;
        }

        private void txtFullName_Validating(object sender, CancelEventArgs e) {
            string errMessage;
            if (!ValidateFullName(txtFullName.Text, out errMessage) && errFullName.GetError(txtFullName) == "") {
                e.Cancel = true;
                txtFullName.Select(0, txtFullName.Text.Length);
                this.errFullName.SetError(txtFullName, errMessage);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e) {
            string errMessage;
            if (!ValidateEmail(txtEmail.Text, out errMessage) && errEmail.GetError(txtEmail) == "") {
                e.Cancel = true;
                txtEmail.Select(0, txtEmail.Text.Length);
                this.errEmail.SetError(txtEmail, errMessage);
            }
        }

        private void txtEmail_Validated(object sender, EventArgs e) {
            errEmail.SetError(txtEmail, "");
        }

        private void txtFullName_Validated(object sender, EventArgs e) {
            errFullName.SetError(txtFullName, "");
        }

        private void EditAccount_Load(object sender, EventArgs e) {
            this.txtUserName.ReadOnly = true;
        }

        private void setFields() {
            try {
                // get account
                RentIt.Account acct = rentIt.ValidateCredentials(credentials);
                this.account = acct;
                txtUserName.Text = acct.UserName;
                txtFullName.Text = acct.FullName;
                txtEmail.Text = acct.Email;
            }
            catch {
                txtUserName.Text = "Unable to retrieve account";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e) {
            if(ValidateAll()) {
                if(txtPassword.Text != "") {
                    SHA1 sha1 = SHA1.Create();
                    string hp = GetSHA1Hash(sha1, txtPassword.Text);
                    account.HashedPassword = hp;
                }
                account.FullName = txtFullName.Text;
                account.Email = txtEmail.Text;
                try {
                    rentIt.UpdateAccountInfo(credentials, account);
                }
                catch {
                    MessageBox.Show("Something went wrong :( Try again.");
                }
                setFields();
            }
        }

        private bool ValidateAll() {
            string errMessage;

            // password match
            if (txtPasswordConfirm.Text != txtPassword.Text) {
                txtPasswordConfirm.Select(0, txtPasswordConfirm.Text.Length);

                this.errPassword.SetError(txtPassword, "Passwords do not match");
                return false;
            }

            // fullname
            if (!ValidateFullName(txtFullName.Text, out errMessage)) {
                txtFullName.Select(0, txtFullName.Text.Length);

                this.errFullName.SetError(txtFullName, errMessage);
                return false;
            }

            // email
            if (!ValidateEmail(txtEmail.Text, out errMessage) && errEmail.GetError(txtEmail) == "") {
                txtEmail.Select(0, txtEmail.Text.Length);

                this.errEmail.SetError(txtEmail, errMessage);
                return false;
            }

            return true;
        }

        // get sha1 hash as string from string
        private static string GetSHA1Hash(SHA1 sha1Hash, string input) {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
