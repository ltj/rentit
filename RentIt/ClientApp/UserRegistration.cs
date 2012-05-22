namespace ClientApp
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;
    using System.Text.RegularExpressions;
    using System.Security.Cryptography;

    using RentIt;

    /// <summary>
    /// RentItUserControl to display user registration and login forms
    /// </summary>
    internal partial class UserRegistration : RentItUserControl
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// User name validating event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string errMessage;
            if (!ValidateUserName(txtUserName.Text, out errMessage) && errUserName.GetError(txtUserName) == "")
            {
                e.Cancel = true;
                txtUserName.Select(0, txtUserName.Text.Length);

                this.errUserName.SetError(txtUserName, errMessage);
            }
        }

        /// <summary>
        /// user name validated event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_Validated(object sender, EventArgs e)
        {
            errUserName.SetError(txtUserName, "");
        }

        // full name validating event handler
        private void txtFullName_Validating(object sender, CancelEventArgs e)
        {
            string errMessage;
            if (!ValidateFullName(txtFullName.Text, out errMessage) && errFullName.GetError(txtFullName) == "")
            {
                e.Cancel = true;
                txtFullName.Select(0, txtFullName.Text.Length);
                this.errFullName.SetError(txtFullName, errMessage);
            }
        }

        /// <summary>
        /// full name validated event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFullName_Validated(object sender, EventArgs e)
        {
            errFullName.SetError(txtFullName, "");
        }


        /// <summary>
        /// validator function for Full name field
        /// </summary>
        /// <param name="name"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateFullName(string name, out string errorMessage)
        {
            if (name == "")
            {
                errorMessage = "Please enter your name";
                return false;
            }

            if (Regex.IsMatch(name, @"^[\w\s]+?$"))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please only use alphanumerics and spaces";
            return false;
        }

        /// <summary>
        /// validate user name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateUserName(string name, out string errorMessage)
        {
            if (name == "")
            {
                errorMessage = "Please enter a username";
                return false;
            }

            if (Regex.IsMatch(name, @"^[\w\d]+?$"))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please only use alphanumerics";
            return false;
        }

        /// <summary>
        /// Password confirmation validating event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPasswordConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasswordConfirm.Text != txtPassword.Text && errPassword.GetError(txtPassword) == "")
            {
                e.Cancel = true;
                txtPasswordConfirm.Select(0, txtPasswordConfirm.Text.Length);

                this.errPassword.SetError(txtPassword, "Passwords do not match");
            }
        }

        /// <summary>
        /// password conformation validated event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPasswordConfirm_Validated(object sender, EventArgs e)
        {
            errPassword.SetError(txtPassword, "");
        }

        /// <summary>
        /// Email validation helper method
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateEmail(string mail, out string errorMessage)
        {
            if (mail == "")
            {
                errorMessage = "Please enter an email address";
                return false;
            }

            if (Regex.IsMatch(mail, @"^[\w\d\.-_]+@([\w\d-_]+?\.)+\w{2,4}$"))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Please enter a valid email address";
            return false;
        }

        /// <summary>
        /// email validating event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string errMessage;
            if (!ValidateEmail(txtEmail.Text, out errMessage) && errEmail.GetError(txtEmail) == "")
            {
                e.Cancel = true;
                txtEmail.Select(0, txtEmail.Text.Length);
                this.errEmail.SetError(txtEmail, errMessage);
            }
        }

        /// <summary>
        /// email validated event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmail_Validated(object sender, EventArgs e)
        {
            errEmail.SetError(txtEmail, "");
        }

        /// <summary>
        /// password validating event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text == "" && errPassword.GetError(txtPassword) == "")
            {
                e.Cancel = true;
                txtPassword.Select(0, txtPassword.Text.Length);
                this.errPassword.SetError(txtPassword, "Please enter a password");
            }
        }

        /// <summary>
        /// password validated event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_Validated(object sender, EventArgs e)
        {
            errPassword.SetError(txtPassword, "");
        }

        /// <summary>
        /// registration button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateAll())
            {

                SHA1 sha1 = SHA1.Create();
                string hash = GetSHA1Hash(sha1, txtPassword.Text);

                RentIt.Account acct = new RentIt.Account
                {
                    UserName = txtUserName.Text,
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    HashedPassword = hash
                };

                try
                {
                    this.RentItProxy.CreateNewUser(acct);
                    MessageBox.Show("User " + acct.UserName + " successfully created. Please log in.");
                    txtUserName.Clear();
                    txtEmail.Clear();
                    txtFullName.Clear();
                    txtPassword.Clear();
                    txtPasswordConfirm.Clear();
                    
                    // focus login
                    loginUserNameTextBox.Text = acct.UserName;
                    loginPasswordTextBox.Clear();
                    loginPasswordTextBox.Select();
                }
                catch
                {
                    MessageBox.Show("Something went wrong :( Please try again.");
                }
            }

        }

        /// <summary>
        /// get sha1 hash as string from string
        /// </summary>
        /// <param name="sha1Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetSHA1Hash(SHA1 sha1Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// validate all (on clicking register)
        /// </summary>
        /// <returns></returns>
        private bool ValidateAll()
        {
            string errMessage;

            // username
            if (!ValidateUserName(txtUserName.Text, out errMessage))
            {
                txtUserName.Select(0, txtUserName.Text.Length);

                this.errUserName.SetError(txtUserName, errMessage);
                return false;
            }

            // password mising
            if (txtPassword.Text == "")
            {
                txtPassword.Select(0, txtPassword.Text.Length);

                this.errPassword.SetError(txtPassword, "Please enter a password");
                return false;
            }

            // password match
            if (txtPasswordConfirm.Text != txtPassword.Text)
            {
                txtPasswordConfirm.Select(0, txtPasswordConfirm.Text.Length);

                this.errPassword.SetError(txtPassword, "Passwords do not match");
                return false;
            }

            // fullname
            if (!ValidateFullName(txtFullName.Text, out errMessage))
            {
                txtFullName.Select(0, txtFullName.Text.Length);

                this.errFullName.SetError(txtFullName, errMessage);
                return false;
            }

            // email
            if (!ValidateEmail(txtEmail.Text, out errMessage) && errEmail.GetError(txtEmail) == "")
            {
                txtEmail.Select(0, txtEmail.Text.Length);

                this.errEmail.SetError(txtEmail, errMessage);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Login button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SHA1 sha1 = SHA1.Create();
            string hp = GetSHA1Hash(sha1, this.loginPasswordTextBox.Text);
            var credentials = new AccountCredentials()
                {
                    UserName = this.loginUserNameTextBox.Text,
                    HashedPassword = hp
                };

            try
            {
                this.RentItProxy.ValidateCredentials(credentials);
                FireCredentialsChangeEvent(credentials);
            }
            catch (Exception)
            {
                MessageBox.Show("Username or password not correct. Please try again");
            }
        }

        /// <summary>
        /// Enables user to press enter key instead of pressing login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keyEventArgs"></param>
        private void LoginKeyPressed(object sender, KeyEventArgs keyEventArgs) {
            if(keyEventArgs.KeyCode == Keys.Enter) {
                keyEventArgs.Handled = true;
                button1_Click(sender, keyEventArgs);
            }
        }

        /// <summary>
        /// Enables user to press enter key instead of pressing register button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keyEventArgs"></param>
        private void RegisterUserKeyPressed(object sender, KeyEventArgs keyEventArgs) {
            if(keyEventArgs.KeyCode == Keys.Enter) {
                keyEventArgs.Handled = true;
                btnRegister_Click(sender, keyEventArgs);
            }
        }
    }
}
