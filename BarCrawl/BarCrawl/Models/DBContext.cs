using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCrawl.Models
{
    public class DBModelContext : DbContext
    {

        public DbSet<Bar> Bars { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.\\SQLExpress;Database=BarCrawl;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<BarCrawl.Models.Bar> Bar { get; set; }
    }
}

