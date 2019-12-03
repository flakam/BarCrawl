using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class LongAndLat
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public LongAndLat()
        {
        }
        public LongAndLat(JToken u)
        {
                       
            this.Latitude = decimal.Parse(u["coordinates"]["latitude"].ToString());
            this.Longitude = decimal.Parse(u["coordinates"]["longitude"].ToString());
                       
        }
    }
}
