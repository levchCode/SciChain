using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using SciChain.network;
using System.Text;
using System;

namespace SciChain.blockchain
{
    class Blockchain
    {
        public List<Block> blocks;
        P2P p = new P2P();

        public void AddBlock(Block block)
        {
            block.previousHash = blocks[blocks.Count - 1].hash;

            blocks.Add(block);
        }

        public void Save(bool send)
        {
            string json = JsonConvert.SerializeObject(blocks.ToArray());

            if (!File.Exists("chain.json"))
            {
                File.Create("chain.json");
            }

            File.WriteAllText("chain.json", json, Encoding.UTF8);

            if (send)
            {
                p.SendOpenMessage("NEWBLOCKS " + json);
            }
        }


        public void Load()
        {

            if (File.Exists("chain.json"))
            {
                blocks = JsonConvert.DeserializeObject<List<Block>>(File.ReadAllText("chain.json", Encoding.UTF8));
            }
            else
            {
                p.SendOpenMessage("GET");
                string chain = p.Listen();
                File.WriteAllText("chain.json", chain, Encoding.UTF8);
                blocks = JsonConvert.DeserializeObject<List<Block>>(chain);
            }
           
        }

        public void Mine()
        {
            foreach (Block b in blocks)
            {
                b.Mine();
            }

            Save(true);
        }
    }
}
