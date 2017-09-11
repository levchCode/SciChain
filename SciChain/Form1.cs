using System;
using System.Windows.Forms;
using SciChain.blockchain;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace SciChain
{
    public partial class Form1 : Form
    {
        Blockchain bc = new Blockchain();
        public static string username;
        network.P2P p2 = new network.P2P();

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Автор", 100);
            listView1.Columns.Add("Название", 100);
            listView1.Columns.Add("Текст", 100);
        }


    
        private void ListBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            Paper p = (Paper)listBox1.Items[listBox1.SelectedIndex];
            author.Text = p.Author;
            name.Text = p.Name;
            webBrowser1.DocumentText = p.Text;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Thread piThread = new Thread(new ThreadStart(p2.ListenToGive));
            piThread.Start();

            if (File.Exists("user.json"))
            {
                User u = JsonConvert.DeserializeObject<User>(File.ReadAllText("user.json"));
                username = u.login;
                bc.Load();
                
                foreach (Block b in bc.blocks)
                {
                    if (b.data.Author == username)
                    {
                        listBox1.Items.Add(b.data);
                    }
                }
                //listBox1.SetSelected(1, true);

                Thread getNew = new Thread(new ThreadStart(p2.ListenForNew));
                getNew.Start();
            }
            

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            AddPaper a = new AddPaper();
            a.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string q = textBox1.Text; 
            bc.Load();

            ListViewItem l;
            
            string[] arr = new string[3];

            foreach (Block b in bc.blocks)
            {
                arr[0] = b.data.Author;
                arr[1] = b.data.Name;
                arr[2] = b.data.Text;
                l = new ListViewItem(arr);

                if (arr[0].Contains(q))
                {
                    listView1.Items.Add(l);
                }
                else
                {
                    if (arr[1].Contains(q))
                    {
                        listView1.Items.Add(l);
                    }
                    else
                    {
                        if (arr[2].Contains(q))
                        {
                            listView1.Items.Add(l);
                        }
                    }
                }
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Paper pe = new Paper();
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                pe.Author = item.SubItems[0].Text;
                pe.Name = item.SubItems[1].Text;
                pe.Text = item.SubItems[2].Text;
            }
            
            ViewPaper v = new ViewPaper()
            {
                p = pe
            };
            v.Show();
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            richTextBox2.Text += value;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bc.Load();

            Thread mine = new Thread(new ThreadStart(bc.Mine));
            mine.SetApartmentState(ApartmentState.STA);
            mine.Start();
            
        }

    }
}
