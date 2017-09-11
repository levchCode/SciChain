using System;
using System.Windows.Forms;

namespace SciChain
{
    public partial class ViewPaper : Form
    {
        public Paper p;

        public ViewPaper()
        {
            InitializeComponent();
        }

        private void ViewPaper_Load(object sender, EventArgs e)
        {
            label2.Text = p.Author;
            label4.Text = p.Name;
            webBrowser1.DocumentText = p.Text;
        }
    }
}
