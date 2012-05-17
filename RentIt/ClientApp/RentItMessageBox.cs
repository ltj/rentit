namespace ClientApp
{
    using System.Windows.Forms;

    internal class RentItMessageBox
    {
        private static string ERROR_TITLE = "Error";
        private static string NOT_LOGGED_IN_MESSAGE = "You are not logged in.";

        private static string RENTAL_ERROR =
            "The program could not rent the media either because the media is already rented, you do not have enough credits or because there is a communication problem with the service servers.";

        private static string ALREADY_REVIEWED_MESSAGE = "You have already reviewed this media or you are not logged in.";

        private static string INFORMATION_TITLE = "Information";

        private static string RENTAL_CONFIRMATION = "The media was succesfully rented.";

        private static string ACCOUNT_DELETION_ERROR = "The account could not be deleted. Please contact support for further assistance.";
        private static string ACCOUNT_DELETION_SUCCESS = "The account was successfully deleted.";

        public static void NotLoggedIn()
        {
            MessageBox.Show(NOT_LOGGED_IN_MESSAGE, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void AlreadyReviewedItem()
        {
            MessageBox.Show(ALREADY_REVIEWED_MESSAGE, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool CreditConfirmation(int value)
        {
            var result = MessageBox.Show(
                "You are about to add " + value + " Credits. Proceed?",
                "Confirm Credit Amount",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        public static void RentalFailed()
        {
            MessageBox.Show(RENTAL_ERROR, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void SuccesfulRental()
        {
            MessageBox.Show(RENTAL_CONFIRMATION, INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool AccountDeletionConfirmation() {
            var result = MessageBox.Show(
                "Are you sure you want to delete your RentIt account?",
                "Confirm account deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        public static void AccountDeletionFailed() {
            MessageBox.Show(ACCOUNT_DELETION_ERROR, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void AccountDeletionSucceeded() {
            MessageBox.Show(ACCOUNT_DELETION_SUCCESS, INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
