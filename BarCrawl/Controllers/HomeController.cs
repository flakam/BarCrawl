//TEST CHANGE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarCrawl.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using BarCrawl.Data;

namespace BarCrawl.Controllers
{

    public class HomeController : Controller
    {

        public static List<Bar> PossibleBars = new List<Bar>();
        List<Bar> Bars = new List<Bar>();
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult CreateCrawlDetail(string name)
        {
            Crawl c = new Crawl { name = name };
            Barcrawl bc = new Barcrawl();
            List<Barcrawl> listBarcrawl = new List<Barcrawl>();
            List<Bar> bar = PossibleBars;
            foreach (Bar item in bar)
            {
                /*
                listBarcrawl.Add(new Barcrawl
                {
                    bar = item,
                    crawl = c,
                });
                */
                item.barCrawl.Add(new Barcrawl
                {
                    crawl = c
                }
                    );
            }

            

            db.Crawl.Add(c);
            
            foreach(Bar b in bar)
            {
                if (db.Bar.Select(a => a.BarId).Where(id => id == b.BarId).Take(1) == null)
                {
                    db.Bar.Add(b);
                }
            }

            db.SaveChanges();

            return RedirectToAction(nameof(Index));
           
            
        }

        public List<Bar> GetBars(string location, string rating)

        {
            //Get all bars in location
            List<Bar> barList = new List<Bar>();

            for (int i = 0; i < 1000; i+=50)
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search?term=bars&location={location}&rating={rating}&offset={i}&radius=5000&limit=50");
                request.Headers.Add("Authorization", "Bearer 5AZ1TMhzZzb52DbbAMkydLPjNRSURY3x-DtC2o7qDjNTa2n96PSxuLZMmQoBy3WtX5q4EWUh4KQWVG1GG_nq_x2YLEssXjh5WF5kYw8E_VPmyRVMRfDHLwOYM0bXXXYx");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                string ApiText = rd.ReadToEnd();
                JToken tokens = JToken.Parse(ApiText);

                List<JToken> ts = tokens["businesses"].ToList();

                foreach (JToken t in ts)
                {
                    Bar b = new Bar(t);
                    if (double.Parse(b.Rating) >= double.Parse(rating))
                    {
                        barList.Add(b);
                    }
                }
            }
            

            return barList;
        }



        public List<Bar> getCrawlBars(Bar start, int radius, int numBars)
        {
            Random rand = new Random();
            Bar point = start;
            List<Bar> crawlList = new List<Bar>();
            crawlList.Add(point);
            // Get all valid bars
            while (crawlList.Count < numBars)
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search?term=bars&location={point.Location}&radius={radius}");
                request.Headers.Add("Authorization", "Bearer 5AZ1TMhzZzb52DbbAMkydLPjNRSURY3x-DtC2o7qDjNTa2n96PSxuLZMmQoBy3WtX5q4EWUh4KQWVG1GG_nq_x2YLEssXjh5WF5kYw8E_VPmyRVMRfDHLwOYM0bXXXYx");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                string ApiText = rd.ReadToEnd();
                JToken tokens = JToken.Parse(ApiText);

                List<JToken> ts = tokens["businesses"].ToList();
                List<Bar> possibleList = new List<Bar>();
                foreach (JToken t in ts)
                {
                    Bar b = new Bar(t);
                    possibleList.Add(b);
                }


                // RNG choose a close by bar, add to list
                int index = rand.Next(possibleList.Count);
                point = possibleList[index];

                // Add only if it's not already on the list - not working

                bool dup = false;
                foreach (Bar x in crawlList)
                {
                    if (x.BarId == point.BarId)
                    {
                        dup = true;
                    }
                }

                if (!dup)
                {
                    crawlList.Add(point);
                }

            }
            Bars = crawlList;
            //for saving the crawl
            Barcrawl crawl = new Barcrawl();
            return crawlList;
        }


        public IActionResult Stops(string id, string name, string location, double longitude, double latitude, string price, string rating)
        {
            Bar b = new Bar() { BarId = id, Name = name, Location = location, Latitude = latitude, Longitude = longitude, Price = price, Rating = rating};

            List<Bar> posBars = getCrawlBars(b, 1000, 5);

            //Barcrawl bc = new Barcrawl(posBars);
            PossibleBars = posBars;
            return View(posBars);
        }

        public IActionResult Search()
        {
            return View();
        }



        public IActionResult Result(string location, string rating)
        {
            List<Bar> bars = GetBars(location, rating);
            return View(bars);
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




        /*
        public async Task<IActionResult> SaveCrawl(List<Bar> Crawl)
        {
            Barcrawl bc = new Barcrawl();
            bc.crawl = Crawl;
            if (ModelState.IsValid)
            {
                db.Add(bc);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bc);
        }
        
        /*
        public IActionResult SaveBar(string id)
        {
            Bar saveBar = Bars.FirstOrDefault(v => v.Id == id);
            if (saveBar != null)
            {
                db.bar.Add(saveBar);
                db.SaveChanges();

            }
            return View();
        }
        */
    }
}
