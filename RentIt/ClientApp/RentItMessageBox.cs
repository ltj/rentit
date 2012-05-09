namespace ClientApp {
    using System.Windows.Forms;

    class RentItMessageBox {
        private static string ERROR_TITLE = "Error";
        private static string NOT_LOGGED_IN_MESSAGE = "You are not logged in.";

        private static string ALREADY_REVIEWED_MESSAGE = "You have already reviewed this media";

        public static void NotLoggedIn() {
            MessageBox.Show(NOT_LOGGED_IN_MESSAGE, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void AlreadyReviewedItem() {
            MessageBox.Show(ALREADY_REVIEWED_MESSAGE, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
