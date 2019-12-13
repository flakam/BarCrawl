using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    [Table("CrawlTable")]
    public class Crawl
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CrawlID { get; set; }
        public string name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List<CrawlUser> crawlUser { get; set; } = new List<CrawlUser>();
        public List<Barcrawl> barCrawl { get; set; } = new List<Barcrawl>();


    }
}
