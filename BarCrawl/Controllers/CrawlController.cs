using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarCrawl.Data;
using BarCrawl.Models;
using System.Security.Claims;

namespace BarCrawl.Controllers
{
    public class CrawlController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrawlController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crawl = await _context.Crawl
                .FirstOrDefaultAsync(m => m.CrawlID == id);
            if (crawl == null)
            {
                return NotFound();
            }
            else
            {
                CrawlUser cu = new CrawlUser();
               // cu.crawl = crawl;
                cu.usersID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                crawl.crawlUser.Add(cu);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
            //return View(crawl);
        }

        // GET: Crawl
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crawl.ToListAsync());
        }

        // GET: Crawl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crawl = await _context.Crawl
                .FirstOrDefaultAsync(m => m.CrawlID == id);
            if (crawl == null)
            {
                return NotFound();
            }

            return View(crawl);
        }

        // GET: Crawl/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crawl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CrawlID,name,DateCreated")] Crawl crawl)
        {
            crawl.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _context.Add(crawl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crawl);
        }

        // GET: Crawl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crawl = await _context.Crawl.FindAsync(id);
            if (crawl == null)
            {
                return NotFound();
            }
            return View(crawl);
        }

        // POST: Crawl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CrawlID,name,DateCreated")] Crawl crawl)
        {
            if (id != crawl.CrawlID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crawl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrawlExists(crawl.CrawlID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crawl);
        }

        // GET: Crawl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crawl = await _context.Crawl
                .FirstOrDefaultAsync(m => m.CrawlID == id);
            if (crawl == null)
            {
                return NotFound();
            }

            return View(crawl);
        }

        // POST: Crawl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crawl = await _context.Crawl.FindAsync(id);
            _context.Crawl.Remove(crawl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrawlExists(int id)
        {
            return _context.Crawl.Any(e => e.CrawlID == id);
        }
    }
}
