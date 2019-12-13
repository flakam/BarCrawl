using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    [Table("CrawlUser")]
    public class CrawlUser
    { 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public Crawl crawl { get; set; }
        public string usersID { get; set; }
      //  public List<Barcrawl> barCrawl { get; set; } = new List<Barcrawl>();

    }
}
