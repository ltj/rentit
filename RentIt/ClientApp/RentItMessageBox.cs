namespace ClientApp {
    using System.Windows.Forms;

    class RentItMessageBox {
        private static string ERROR_TITLE = "Error";
        private static string NOT_LOGGED_IN_MESSAGE = "You are not logged in.";

        public static void NotLoggedIn() {
            MessageBox.Show(NOT_LOGGED_IN_MESSAGE, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool CreditConfirmation(int value) {
            var result = MessageBox.Show(
                "You are about to add " + value + " Credits. Proceed?",
                "Confirm Credit Amount",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        	return result == DialogResult.Yes;
        }
    }
}
