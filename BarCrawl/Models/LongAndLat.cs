using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class LongAndLat
    {
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public decimal ShowLa { get { return Latitude; } }
        public decimal ShoLo { get { return Longitude; } }

        public LongAndLat()
        {
        }
        public LongAndLat(JToken u)
        {
            this.Name = u["name"].ToString();           
            this.Latitude = decimal.Parse(u["coordinates"]["latitude"].ToString());
            this.Longitude = decimal.Parse(u["coordinates"]["longitude"].ToString());
                       
        }
    }
}
