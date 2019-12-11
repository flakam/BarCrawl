using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class Bar
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Alias { get; set; }

        public Bar()
        {

        }

        public Bar(JToken t)
        {
            this.Id = t["id"].ToString();
            this.Name = t["name"].ToString();
            this.Alias = t["alias"].ToString();
            this.Latitude = double.Parse(t["coordinates"]["latitude"].ToString());
            this.Longitude = double.Parse(t["coordinates"]["longitude"].ToString());
            this.Location = t["location"]["display_address"].ToString();
            if (t["price"] != null)
            {
                this.Price = t["price"].ToString();
            }
            else
            {
                this.Price = "NA";
            }
        }
    }


}
