﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class Bar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string BarId { get; set; }
        public string Name { get; set; }
        //public double Longitude { get; set; }
        //public double Latitude { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public string Url { get; set; }
        public List<Barcrawl> barCrawl { get; set; } = new List<Barcrawl>();
        public string Id { get; internal set; }
        public string PicUrl { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Bar()
        {

        }

        public Bar(JToken t)
        {
            this.BarId = t["id"].ToString();
            this.Name = t["name"].ToString();
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

            this.Rating = t["rating"].ToString();
            this.Url = t["url"].ToString();
            if (t["PicUrl"] != null)
            {
                this.PicUrl = t["PicUrl"].ToString();
            }
            else
            {
                this.PicUrl = "NA";
            }
        }
    }


}
