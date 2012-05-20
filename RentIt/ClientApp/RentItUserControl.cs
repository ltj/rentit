namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Per Mortensen</author>
    /// <summary>
    /// The class serves as the base class for all the UserControls that is to be
    /// utilizes in the client application. It declares a set of events and methods,
    /// most notably the events for content, credentials and credits changes, that
    /// the MainForm subscribes to each time it adds a RentItUserControl to its
    /// ContentPane.
    /// </summary>
    internal class RentItUserControl : UserControl
    {
        /// <summary>
        /// Auto-property for getting and setting the RentItClient instance.
        /// </summary>
        internal virtual RentItClient RentItProxy { get; set; }

        /// <summary>
        /// Auto-property for getting and setting the credentials of the user.
        /// </summary>
        internal virtual AccountCredentials Credentials { get; set; }

        /// <summary>
        /// This event is fired every time the currently displayed UserControl 
        /// in the MainForm's ContentPane or the TopBar must change the contents
        /// of the MainForm's ContentPane. 
        /// </summary>
        public event ContentChangeEventHandler ContentChangeEvent;

        /// <summary>
        /// This event is fired every time the currently displayed UserControl 
        /// in the MainForm's ContentPane or the TopBar must change the credentials
        /// to be stored in MainForm. This happens every time a user either logs in or
        /// log out.
        /// </summary>
        internal event CredentialsChangeEventHandler CredentialsChangeEvent;

        /// <summary>
        /// This event is fired every time a logged in user rents a products.
        /// It is used to inform the TopBar that the shown balance must be
        /// updated to reflect the purchase of the newly acquired rental.
        /// </summary>
        internal event CreditsChangeEventHandler CreditsChangeEvent;

        /// <summary>
        /// Fires a content change event to change the current
        /// content of the main form with the one specified in
        /// control.
        /// </summary>
        /// <param name="control">
        /// The new RentItUserControl to display as main content.
        /// </param>
        /// <param name="title">
        /// The new top bar title.
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
        /// Fires a credits change event to change the currently 
        /// displayed credits in the top bar.
        /// </summary>
        /// <param name="credits"></param>
        protected void FireCreditsChangeEvent(int credits)
        {
            if (CreditsChangeEvent != null)
                CreditsChangeEvent(this, new CreditsChangeArgs(credits));
        }

        /// <summary>
        /// For propagating the MainForms subscription to the ContentChangeEvent
        /// to the RentItUserControl's inner RentItUserControls.
        /// </summary>
        protected void ContentChangeEventPropagated(object obj, ContentChangeArgs e)
        {
            FireContentChangeEvent(e.NewControl, e.NewTitle);
        }

        /// <summary>
        /// For propagating the MainForms subscription to the CredentialsChangeEvent
        /// to the RentItUserControl's inner RentItUserControls.
        /// </summary>
        protected void CredentialsChangeEventPropagated(object obj, CredentialsChangeArgs e)
        {
            FireCredentialsChangeEvent(e.Credentials);
        }

        /// <summary>
        /// For propagating the MainForms subscription to the CreditsChangeEvent
        /// to the RentItUserControl's inner RentItUserControls.
        /// </summary>
        protected void CreditsChangeEventPropagated(object sender, CreditsChangeArgs e)
        {
            FireCreditsChangeEvent(e.Credits);
        }
    }

    /// <summary>
    /// The EventHandler of the CredentialsChangeEvent.
    /// </summary>
    internal delegate void CredentialsChangeEventHandler(object sender, CredentialsChangeArgs args);

    /// <summary>
    /// A subtype of EventArgs that provides an AccountCredentials.
    /// </summary>
    internal class CredentialsChangeArgs : EventArgs
    {
        public readonly AccountCredentials Credentials;

        public CredentialsChangeArgs(AccountCredentials credentials)
        {
            Credentials = credentials;
        }
    }

    /// <summary>
    /// The EventHandler of the ContenChangeEvent.
    /// </summary>
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

    /// <summary>
    /// EventHandler of the CreditsChangeEvent.
    /// </summary>
    internal delegate void CreditsChangeEventHandler(object sender, CreditsChangeArgs args);

    /// <summary>
    /// A subtype of EventArgs that provides an amount of credits.
    /// </summary>
    internal class CreditsChangeArgs : EventArgs
    {
        public readonly int Credits;

        public CreditsChangeArgs(int credits)
        {
            Credits = credits;
        }
    }
}
