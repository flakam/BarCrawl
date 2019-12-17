using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarCrawl.Models;

namespace BarCrawl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BarCrawl.Models.Barcrawl> Barcrawl { get; set; }
        public DbSet<BarCrawl.Models.Crawl> Crawl { get; set; }
        public DbSet<BarCrawl.Models.Bar> Bar { get; set; }
        public DbSet<BarCrawl.Models.CrawlUser> CrawlUser { get; set; }
    }
}
