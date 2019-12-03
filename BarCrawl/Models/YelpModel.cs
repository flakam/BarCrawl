using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class YelpModel
    {
       

        public List<string> Categories { get; set; }
        public string Alias { get; set; }
        public decimal Longitude { get; set; }
     
        public string Name { get; set; }
        public decimal Latitude{ get; set; }


        public List<string> LocationList { get; set; }
        public string City { get; set; }
       
       
        public YelpModel()
        {
        }
        public YelpModel(JToken u)
        {
           
            this.Alias = u["alias"].ToString();
                  
            this.Name = u["name"].ToString();
            this.Latitude = decimal.Parse(u["coordinates"]["latitude"].ToString());
            this.Longitude = decimal.Parse(u["coordinates"]["longitude"].ToString());


                                 

        }
    }

}

