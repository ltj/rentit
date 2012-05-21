namespace ClientApp
{
    using System.Windows.Forms;

    /// <summary>
    /// Provides a number of standard message boxes for presenting to the user.
    /// </summary>
    internal class RentItMessageBox
    {
        private static string ERROR_TITLE = "Error";
        private static string NOT_LOGGED_IN_MESSAGE = "You are not logged in.";

        #region ErrorMessages

        private static string RENTAL_ERROR =
            "The program could not rent the media either because the media is already rented, you do not have enough credits or because there is a communication problem with the service servers.";

        private static string ALREADY_REVIEWED_MESSAGE = "You have already reviewed this media or you are not logged in.";

        private static string INFORMATION_TITLE = "Information";

        private static string RENTAL_CONFIRMATION = "The media was succesfully rented.";

        private static string ACCOUNT_DELETION_ERROR = "The account could not be deleted. Please contact support for further assistance.";
        private static string ACCOUNT_DELETION_SUCCESS = "The account was successfully deleted.";

        private static string CONNECTION_ERROR = "The program experienced an error while communicatong with the server. Check that you have access to the Internet or try again later.";

        private static string DELETE_MEDIA_FAILED = "Deletion of the media failed due to server communication error. Try again.";

        private static string PRICE_CHANGE_FAILED = "The price change failed due to a server communication error. Try again";

        #endregion

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

        public static bool AccountDeletionConfirmation()
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete your RentIt account?",
                "Confirm account deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Displays a message box informing the user that the 
        /// account deletion failed.
        /// </summary>
        public static void AccountDeletionFailed()
        {
            MessageBox.Show(ACCOUNT_DELETION_ERROR, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a message box informing the user that the 
        /// account deletion succeeded.
        /// </summary>
        public static void AccountDeletionSucceeded()
        {
            MessageBox.Show(ACCOUNT_DELETION_SUCCESS, INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays a message box informing the user that the 
        /// application experienced an error while communicating with
        /// the database. Might be used if the user lost access to the
        /// internet.
        /// </summary>
        public static void ServerCommunicationError()
        {
            MessageBox.Show(CONNECTION_ERROR, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a message box informing the user that the
        /// deletion of the media failed.
        /// </summary>
        public static void DeleteMediaFailed()
        {
            MessageBox.Show(DELETE_MEDIA_FAILED, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a message box informing the user that the
        /// change of the price failed.
        /// </summary>
        public static void PriceChangeFailed()
        {
            MessageBox.Show(PRICE_CHANGE_FAILED, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
