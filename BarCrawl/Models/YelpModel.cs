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
        public string Title { get; set; }
     
        public string Name { get; set; }
       
       
        public List<string> LocationList { get; set; }
        public string City { get; set; }
       
       
        public YelpModel()
        {
        }
        public YelpModel(JToken u)
        {
           
            this.Alias = u["alias"].ToString();
           
            this.Categories = new List<string>();
            if (u["categories"] != null)
            {
                foreach (JToken categories in u["categories"].ToList())
                {
                    this.Categories.Add(categories.ToString());
                }
            }
            this.Title = u["title"].ToString();
           
            this.Name = u["name"].ToString();
           
            
            
            this.LocationList = new List<string>();
            if (u["location"] != null)
            {
                foreach (JToken locations in u["location"].ToList())
                {
                    this.LocationList.Add(locations.ToString());
                }
            }
            this.City = u["city"].ToString();
                    
           
        }
    }

}

