using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace SciChain
{
    public partial class register : Form
    {
        public string username;
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("user.json", JsonConvert.SerializeObject(new User(textBox1.Text, String.Empty)));
            username = textBox1.Text;
            Form1 f = new Form1();
            f.Show();
            Hide();
        }
    }
}
