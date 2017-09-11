using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciChain
{
   public class Paper
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
