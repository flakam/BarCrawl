using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 

namespace BarCrawl.Models
    {
        public class GoogleMapsModel
        {

            public int Id { get; set; }



            public int Latitude { get; set; }

            public int Longitude { get; set; }

            public string Text { get; set; }
            public int Value { get; set; }

            public string StartAddress { get; set; }

            public string EndAddress { get; set; }
            public string HTMLInstructions { get; set; }
            public string Maneuver { get; set; }
            public string Points { get; set; }
            //public List<string> StartLocation { get; set; }
            //public List<string> EndLocation { get; set; }
            //public List<string> Steps { get; set; }
            public List<string> Routes { get; set; }
            //public List<string> Duration { get; set; }
            //public List<string> Polyline { get; set; }


            public GoogleMapsModel()
            {

            }
            public GoogleMapsModel(JToken u)
            {
                this.Latitude = int.Parse(u["lat"].ToString());
                this.Longitude = int.Parse(u["lng"].ToString());
                this.Value = int.Parse(u["value"].ToString());
                this.Text = u["text"].ToString();
                this.StartAddress = u["start_address"].ToString();
                this.EndAddress = u["end_address"].ToString();
                this.HTMLInstructions = u["html_instructions"].ToString();
                this.Maneuver = u["maneuver"].ToString();
                this.Points = u["points"].ToString();

            }

        }
} 

