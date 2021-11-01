using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<NonStandardWorks> NonStandardWorks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<WarehouseCounting> WarehouseCountings { get; set; }
        public DbSet<WeeklyWorkSchedule> WeeklyWorkSchedules { get; set; }

       
    }
}
