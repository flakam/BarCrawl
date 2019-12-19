using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace BarCrawl.Models
{
    [Table("CrawlTable")]
    public class Crawl
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CrawlID { get; set; }
        public string name { get; set; }
        public string UserID { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime datetime { get; set; }
        public List<CrawlUser> crawlUser { get; set; } = new List<CrawlUser>();
        public List<Barcrawl> barCrawl { get; set; } = new List<Barcrawl>();
        public string Location { get; set; }

        public Crawl()
        {
          //  UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

        }


    }
}
