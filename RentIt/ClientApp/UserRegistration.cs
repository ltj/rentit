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
    public partial class UserRegistration : UserControl {

        private RentItClient client;

        public UserRegistration() {
            InitializeComponent();
        }

        internal RentItClient Proxy {
            set { this.client = value; }
        }

        // user name validating event handler
        private void txtUserName_Validating(object sender, CancelEventArgs e) {
            string errMessage;
            if (!ValidateUserName(txtUserName.Text, out errMessage) && errUserName.GetError(txtUserName) == "") {
                e.Cancel = true;
                txtUserName.Select(0, txtUserName.Text.Length);

                this.errUserName.SetError(txtUserName, errMessage);
            }
        }

        // user name validated event handler
        private void txtUserName_Validated(object sender, EventArgs e) {
            errUserName.SetError(txtUserName, "");
        }

        // full name validating event handler
        private void txtFullName_Validating(object sender, CancelEventArgs e) {
            string errMessage;
            if (!ValidateFullName(txtFullName.Text, out errMessage) && errFullName.GetError(txtFullName) == "") {
                e.Cancel = true;
                txtFullName.Select(0, txtFullName.Text.Length);
                this.errFullName.SetError(txtFullName, errMessage);
            }
        }

        // full name validated event handler
        private void txtFullName_Validated(object sender, EventArgs e) {
            errFullName.SetError(txtFullName, "");
        }


        // validator function for Full name field
        private bool ValidateFullName(string name, out string errorMessage) {
            if (name == "") {
                errorMessage = "Please enter your name";
                return false;
            }

            if(Regex.IsMatch(name, @"^[\w\s]+?$")) {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please only use alphanumerics and spaces";
            return false;
        }

        // validate user name
        private bool ValidateUserName(string name, out string errorMessage) {
            if (name == "") {
                errorMessage = "Please enter a username";
                return false;
            }

            if (Regex.IsMatch(name, @"^[\w\d]+?$")) {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please only use alphanumerics";
            return false;
        }

        // Password confirmation validating event handler
        private void txtPasswordConfirm_Validating(object sender, CancelEventArgs e) {
            if (txtPasswordConfirm.Text != txtPassword.Text && errPassword.GetError(txtPassword) == "") {
                e.Cancel = true;
                txtPasswordConfirm.Select(0, txtPasswordConfirm.Text.Length);

                this.errPassword.SetError(txtPassword, "Passwords do not match");
            }
        }

        // password conformation validated event handler
        private void txtPasswordConfirm_Validated(object sender, EventArgs e) {
            errPassword.SetError(txtPassword, "");
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

        // email validating event handler
        private void txtEmail_Validating(object sender, CancelEventArgs e) {
            string errMessage;
            if (!ValidateEmail(txtEmail.Text, out errMessage) && errEmail.GetError(txtEmail) == "") {
                e.Cancel = true;
                txtEmail.Select(0, txtEmail.Text.Length);
                this.errEmail.SetError(txtEmail, errMessage);
            }
        }

        // email validated event handler
        private void txtEmail_Validated(object sender, EventArgs e) {
            errEmail.SetError(txtEmail, "");
        }

        // password validating event handler
        private void txtPassword_Validating(object sender, CancelEventArgs e) {
            if (txtPassword.Text == "" && errPassword.GetError(txtPassword) == "") {
                e.Cancel = true;
                txtPassword.Select(0, txtPassword.Text.Length);
                this.errPassword.SetError(txtPassword, "Please enter a password");
            }
        }

        // password validated event handler
        private void txtPassword_Validated(object sender, EventArgs e) {
            errPassword.SetError(txtPassword, "");
        }

        private void btnRegister_Click(object sender, EventArgs e) {
            if (ValidateAll()) {

                SHA1 sha1 = SHA1.Create();
                string hash = GetSHA1Hash(sha1, txtPassword.Text);

                RentIt.Account acct = new RentIt.Account {
                    UserName = txtUserName.Text,
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    HashedPassword = hash
                };

                try {
                    client.CreateNewUser(acct);
                }
                catch {
                    MessageBox.Show("Something went wrong :( Please try again.");
                }
            }

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

        // validate all (on clicking register)
        private bool ValidateAll() {
            string errMessage;

            // username
            if (!ValidateUserName(txtUserName.Text, out errMessage)) {
                txtUserName.Select(0, txtUserName.Text.Length);

                this.errUserName.SetError(txtUserName, errMessage);
                return false;
            }

            // password mising
            if (txtPassword.Text == "") {
                txtPassword.Select(0, txtPassword.Text.Length);

                this.errPassword.SetError(txtPassword, "Please enter a password");
                return false;
            }

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

    }
}
