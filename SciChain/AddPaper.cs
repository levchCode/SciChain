using System;
using System.Windows.Forms;
using SciChain.blockchain;
using System.IO;
using Spire.Doc;

namespace SciChain
{

    public partial class AddPaper : Form
    {
        
        public static string docText = "";

        public AddPaper()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Document doc = new Document();
            doc.LoadFromFile(openFileDialog1.FileName);
            MemoryStream s = new MemoryStream();

            doc.SaveToFile("temp.html", FileFormat.Html);
            
            docText = File.ReadAllText("temp.html", System.Text.Encoding.UTF8);

            button2.Text = "Файл выбран";
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            Blockchain bc = new Blockchain();
            bc.Load();

            Block b = new Block()
            {
                data = new Paper()
                {
                    Author = Form1.username,
                    Name = textBox1.Text,
                    Text = docText
                }
            };

            b.Hash();

            bc.AddBlock(b);
            bc.Save(false);

            Hide();
        }
    }
}
