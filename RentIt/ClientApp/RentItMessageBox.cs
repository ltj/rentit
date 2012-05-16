namespace ClientApp
{
    using System.Windows.Forms;

    internal class RentItMessageBox
    {
        private static string ERROR_TITLE = "Error";
        private static string NOT_LOGGED_IN_MESSAGE = "You are not logged in.";
        private static string NETWORK_ERROR = "The program cannot establish a connection to the server. Please check your internet connection.";

        private static string ALREADY_REVIEWED_MESSAGE = "You have already reviewed this media or you are not logged in";

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

        public static void ServerCommunicationFailure()
        {
            MessageBox.Show(NETWORK_ERROR, ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
