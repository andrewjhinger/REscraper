using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace REscraper.Models
{
    public class OhioReDbContext : DbContext
    {

        public OhioReDbContext()
            : base("OhioReDbContext")
        {
        }

        public DbSet<REproperty> REproperty { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}