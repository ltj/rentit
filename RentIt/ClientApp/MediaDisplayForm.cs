namespace ClientApp
{
    using System.Windows.Forms;

    /// <summary>
    /// A window for playing/displaying media.
    /// </summary>
    internal partial class MediaDisplayForm : Form
    {
        public MediaDisplayForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the content of this window using a UserControl.
        /// This is designed for the AlbumPlayerControl, BookReaderControl,
        /// and MoviePlayerControl.
        /// </summary>
        internal UserControl Content
        {
            set
            {
                Controls.Clear();
                value.Dock = DockStyle.Fill;
                Controls.Add(value);
            }
        }
    }
}
