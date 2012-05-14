namespace ClientApp {
    using System;
    using System.Windows.Forms;

    using RentIt;

    public class RentItUserControl : UserControl {
        internal virtual RentItClient RentItProxy { get; set; }
        internal virtual AccountCredentials Credentials { get; set; }

        public event ContentChangeEventHandler ContentChangeEvent;

        /// <summary>
        /// Fires a content change event to change the current
        /// content of the main form with the one specified in
        /// control.
        /// </summary>
        /// <param name="control">
        /// The new RentItUserControl to display as main content.
        /// </param>
        protected void FireContentChangeEvent(RentItUserControl control, string title) {
            if(ContentChangeEvent != null)
                ContentChangeEvent(this, new ContentChangeArgs(control, title));
        }
    }

    public delegate void ContentChangeEventHandler(object sender, ContentChangeArgs args);

    /// <summary>
    /// A subtype of EventArgs that provides a RentItUserControl.
    /// </summary>
    public class ContentChangeArgs : EventArgs {
        public readonly RentItUserControl NewControl;
        public readonly string NewTitle;

        public ContentChangeArgs(RentItUserControl newControl, string newTitle) {
            NewControl = newControl;
            NewTitle = newTitle;
        }
    }
}
