//TEST CHANGE
using Microsoft.AspNetCore.Identity;
using System;
using System.Globalization;
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
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;


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
            ViewBag.Upcoming = UpcomingCrawls();
            return View();
        }

        public List<Crawl> UpcomingCrawls()
        {
            List<Crawl> upcomingCrawls = db.Crawl.Where(x => x.datetime < DateTime.Now.AddDays(30)).ToList();


            return upcomingCrawls;

        }

        public IActionResult UserPage()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Crawl> hey = db.Crawl.Where(a => a.UserID == UserId).ToList();
            return View(hey);
        }

        public IActionResult JoinedCrawls()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<CrawlUser> cu = db.CrawlUser.Include(g => g.crawl).Where(a => a.usersID == UserId).ToList();


            return View(cu);
        }

        public IActionResult CrawlDetails(string ID)
        {
            int crawlID = int.Parse(ID);
            ViewBag.CrawlName = db.Crawl.Find(crawlID).name;
            List<Barcrawl> bc = db.Barcrawl.Include(g => g.bar).Where(a => a.crawl.CrawlID == crawlID/*int.Parse(crawlID)*/).ToList();
            List<Bar> cool = new List<Bar>();
            List<string> users = new List<string>();
            foreach(Barcrawl ayy in bc)
            {

                Bar b = new Bar { Name = ayy.bar.Name, Location = ayy.bar.Location };
                cool.Add(b);

            }

            List<CrawlUser> cu = db.CrawlUser.Where(x => x.crawl.CrawlID == crawlID).ToList();
            foreach (CrawlUser crus in cu)
            {
                string un = db.Users.Find(crus.usersID).UserName;
                users.Add(un);
            }
            ViewBag.Users = users;

            ViewBag.MapBars = cool;
            return View(bc);


            //Crawl c = db.Crawl.FirstOrDefault(i => i.CrawlID == 14);

            //List<Bar> bars = new List<Bar>();
            //foreach(Barcrawl bc in c.barCrawl)
            //{
            //    Bar b = db.Bar.Find(bc.bar.BarId);
            //    bars.Add(b);
            //}
            //return View(c);

        }



        [HttpPost]

        public IActionResult CreateCrawlDetail(string name, DateTime crawlDate)

        {
            Crawl c = new Crawl { name = name, datetime = crawlDate };
            c.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Barcrawl bc = new Barcrawl();
            List<Barcrawl> listBarcrawl = new List<Barcrawl>();
            List<Bar> bar = PossibleBars;

            foreach (Bar b in bar)
            {
                if (!db.Bar.Where(x => x.BarId == b.BarId).Any())
                {
                    db.Bar.Add(b);
                }
                c.barCrawl.Add(new Barcrawl
                {
                    BarId = b.BarId,
                    CrawlID = c.CrawlID,

                }
                    );
            }
            string[] bob = bar[0].Location.Split(',');

            string qwer = bob[1] + bob[2];
            string qwert = qwer.Replace("]", "");
            string qwerty = qwert.Replace("\"", "");
            string[] qwertyu = qwerty.Split(' ');
            c.Location = qwertyu[2] + ", " + qwertyu[3];
          



            db.Crawl.Add(c);

            

            db.SaveChanges();
            string id = c.CrawlID.ToString();

            return RedirectToAction("CrawlDetails", new { id = id});
                


        }

        public List<Bar> GetBars(string location, string rating,string price)

        {
            //Get all bars in location
            List<Bar> barList = new List<Bar>();


            for (int i = 0; i < 250; i+=50)
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://api.yelp.com/v3/businesses/search?term=bars&location={location}&price={price}&rating={rating}&radius=5000&offset={i}&limit=50");
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

                // Add only if it's not already on the list 

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


        public IActionResult Stops(string id, string name, string location, double longitude, double latitude, string price, string rating, string url, int num)
        {
            Bar b = new Bar() { BarId = id, Name = name, Location = location, Latitude = latitude, Longitude = longitude, Price = price, Rating = rating, Url = url };

            List<Bar> posBars = getCrawlBars(b, 1200, num);

            //Use stringbuilder to make string for the gmaps url

            StringBuilder waypointsSB = new StringBuilder();
            for (int i = 1; i < posBars.Count() - 2; i++)
            {
                waypointsSB.Append(posBars[i].Name + " " + posBars[i].Location + '|');
            }
            string waypoints = waypointsSB.ToString();

            // Make string play nice with url (no &)

            string waypoints2 = waypoints.Replace("&", "and");

            ViewBag.waypoints = waypoints2;
            PossibleBars = posBars;
            return View(posBars);
        }

        public IActionResult Create()
        {
            return View();
        }



        public IActionResult Result(string city, string state, string rating, string datetime,string price, int numBars)
        {
            string location = city + ", " + state;
            ViewBag.num = numBars;
            List<Bar> bars = GetBars(location, rating,price);
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
    }
}



