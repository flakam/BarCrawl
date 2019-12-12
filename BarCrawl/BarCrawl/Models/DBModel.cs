using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class DBModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
       
       
    }
}
