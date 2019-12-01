using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarCrawl.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BarCrawl.Controllers
{
    public class HomeController : Controller
    {

        public List<YelpModel> GetBars(string location)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search+{location}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string ApiText = rd.ReadToEnd();

            JToken tokens = JToken.Parse(ApiText);

            List<JToken> ts = tokens["results"].ToList();

            List<YelpModel> bars = new List<YelpModel>();

            foreach (JToken t in ts)
            {
                YelpModel a = new YelpModel(t);
                bars.Add(a);
            }

            return bars;
        }
        public IActionResult Result(string location)
        {
            List<YelpModel> bars = GetBars(location);
            return View(bars);
        }
      /*  public string CallAPI(string location)
        {
          
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search+{location}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();
            return APIText;

        }
        public string CallYelp(string term)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search+{term}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();
            return APIText;


        }
        public JToken Parseyelp(string text)
        {
            JToken output = JToken.Parse(text);
            return output;
        }
        public IActionResult Planet(string location)
        {
            string yelpdata = CallAPI(location);
            JToken t = JToken.Parse(yelpdata);
            YelpModel p= new YelpModel(t);
            return View();
        }*/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
       
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
