using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SciChain.blockchain
{
    class Block
    {
        public int Nonce;
        public Paper data { get; set; }
        public string hash;
        public string previousHash;

        
        public void Hash()
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(data.Text + data.Author + data.Name + previousHash + Nonce));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            hash =  Sb.ToString();
        }

        
        public void Mine()
        {
            Form1 f = (Form1)Form1.ActiveForm;

            while (true)
            {
                if (hash.StartsWith("0000"))
                {
                    f.AppendTextBox("Обработано: hash: " + hash + "nonce: " + Nonce + "\n");
                    break;
                }
                else
                {
                    Nonce++;
                }

                Hash();
            }

        }

    }
}
