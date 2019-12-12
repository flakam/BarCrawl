using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarCrawl.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<BarCrawl.Models.Bar> Bar { get; set; }
        
    }
}
