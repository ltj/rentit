namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

    internal class RentItUserControl : UserControl
    {
        internal virtual RentItClient RentItProxy { get; set; }
        internal virtual AccountCredentials Credentials { get; set; }

        public event ContentChangeEventHandler ContentChangeEvent;

        internal event CredentialsChangeEventHandler CredentialsChangeEvent;

        /// <summary>
        /// Fires a content change event to change the current
        /// content of the main form with the one specified in
        /// control.
        /// </summary>
        /// <param name="control">
        /// The new RentItUserControl to display as main content.
        /// </param>
        protected void FireContentChangeEvent(RentItUserControl control, string title)
        {
            if (ContentChangeEvent != null)
                ContentChangeEvent(this, new ContentChangeArgs(control, title));
        }

        /// <summary>
        /// Fires a credentials change event to change the currently 
        /// stored account credentials in the main form.
        /// </summary>
        /// <param name="credentials"></param>
        protected void FireCredentialsChangeEvent(AccountCredentials credentials)
        {
            if (CredentialsChangeEvent != null)
                CredentialsChangeEvent(this, new CredentialsChangeArgs(credentials));
        }

        /// <summary>
        /// For propagating the MainForms subscription to the ContentChangeEvent
        /// to the media players.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void ContentChangeEventPropagated(object obj, ContentChangeArgs e) {
            FireContentChangeEvent(e.NewControl, e.NewTitle);
        }
    }

    internal delegate void CredentialsChangeEventHandler(object sender, CredentialsChangeArgs args);

    /// <summary>
    /// A subtype of EventArgs that provides an AccountCredentials.
    /// </summary>
    internal class CredentialsChangeArgs : EventArgs
    {
        public readonly AccountCredentials credentials;

        public CredentialsChangeArgs(AccountCredentials credentials)
        {
            this.credentials = credentials;
        }
    }


    internal delegate void ContentChangeEventHandler(object sender, ContentChangeArgs args);

    /// <summary>
    /// A subtype of EventArgs that provides a RentItUserControl.
    /// </summary>
    internal class ContentChangeArgs : EventArgs
    {
        public readonly RentItUserControl NewControl;
        public readonly string NewTitle;

        public ContentChangeArgs(RentItUserControl newControl, string newTitle)
        {
            NewControl = newControl;
            NewTitle = newTitle;
        }
    }
}
