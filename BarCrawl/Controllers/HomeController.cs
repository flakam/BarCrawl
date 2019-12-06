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
using System.Net.Http;

namespace BarCrawl.Controllers
{
    public class HomeController : Controller
    {

        public List<YelpModel> GetBars()
        {
        
                HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search?term=bars&location=grand rapids&limit=50&open_at=1575565800&offset=150");
                request.Headers.Add("Authorization", "Bearer 5AZ1TMhzZzb52DbbAMkydLPjNRSURY3x-DtC2o7qDjNTa2n96PSxuLZMmQoBy3WtX5q4EWUh4KQWVG1GG_nq_x2YLEssXjh5WF5kYw8E_VPmyRVMRfDHLwOYM0bXXXYx");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                string ApiText = rd.ReadToEnd();
                JToken tokens = JToken.Parse(ApiText);


                List<JToken> ts = tokens["businesses"].ToList();

                List<YelpModel> yelp = new List<YelpModel>();

                foreach (JToken t in ts)
                {
                    YelpModel a = new YelpModel(t);
                    yelp.Add(a);
                }

                return yelp;
           
           
        }
        public IActionResult Identity()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Result()
        {
            List<YelpModel>bars = GetBars();
            return View(bars);
        }
       
        public  List<YelpModel> GetLongandLat(YelpModel start, int radius, int numBars)
        {
            YelpModel point = start;
            Random rand = new Random();
            
            List<YelpModel> crawlList = new List<YelpModel>();
            crawlList.Add(point);

            while (crawlList.Count < numBars)
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search?term=bars&location={point.Location}&radius={radius}");
                request.Headers.Add("Authorization", "Bearer 5AZ1TMhzZzb52DbbAMkydLPjNRSURY3x-DtC2o7qDjNTa2n96PSxuLZMmQoBy3WtX5q4EWUh4KQWVG1GG_nq_x2YLEssXjh5WF5kYw8E_VPmyRVMRfDHLwOYM0bXXXYx");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                string ApiText = rd.ReadToEnd();
                JToken tokens = JToken.Parse(ApiText); 

                List<JToken> ts = tokens["businesses"].ToList();

                List<YelpModel> longlat = new List<YelpModel>();

                foreach (JToken t in ts)
                {
                    YelpModel b = new YelpModel(t);
                    longlat.Add(b);
                }
                
                int index = rand.Next(longlat.Count);
                point = longlat[index];

                bool dup = false;
                foreach (YelpModel x in crawlList)
                {
                    if (x.Id == point.Id)
                    {
                        dup = true;
                    }
                }

                if (!dup)
                {
                    crawlList.Add(point);
                }
            }
            return crawlList;
        }
           
        public IActionResult Chosen5(string name, string location, decimal longitude, decimal latitude)
        {
           
           YelpModel b = new YelpModel() { Name = name, Location = location, Latitude = latitude, Longitude = longitude };
            List<YelpModel> posBars = GetLongandLat(b, 1000, 5);

            return View(posBars);
        }
        public IActionResult Start()
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
