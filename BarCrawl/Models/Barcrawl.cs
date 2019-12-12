using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    [Table("BarCrawlTable")]
    public class Barcrawl
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Bar bar { get; set; }
        public Crawl crawl { get; set; }

        public Barcrawl()
        {
            
        }
        /*
        public Barcrawl(List<Bar> crawl)
        {
            this.crawl = crawl;
        }
        */
    }
}
