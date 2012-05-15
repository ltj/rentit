
namespace ClientApp
{
    using System.Windows.Forms;

    internal partial class MediaDisplayForm : Form
    {
        public MediaDisplayForm()
        {
            InitializeComponent();
        }

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
