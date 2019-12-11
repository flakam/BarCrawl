using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class Barcrawl
    {
        public List<Bar> crawl = new List<Bar>();

        public Barcrawl()
        {
            
        }

        public Barcrawl(List<Bar> crawl)
        {
            this.crawl = crawl;
        }
    }
}
