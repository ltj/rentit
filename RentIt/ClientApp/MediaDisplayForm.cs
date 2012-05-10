using System.Windows.Forms;

namespace ClientApp {
    public partial class MediaDisplayForm : Form {
        public MediaDisplayForm() {
            InitializeComponent();
        }

        internal UserControl Content {
            set {
                Controls.Clear();
                value.Dock = DockStyle.Fill;
                Controls.Add(value);
            }
        }
    }
}
