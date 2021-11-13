using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromiCRM.Configurations.Entities;
using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        private readonly UserManager<ApiUser> _userManager;
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        {
        }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<NonStandardWork> NonStandardWorks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<WarehouseCounting> WarehouseCountings { get; set; }
        public DbSet<WeeklyWorkSchedule> WeeklyWorkSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string ADMIN_ID = "c9490c27-1b89-4e39-8f2e-99b48dcc709e";
            string ROLE_ID = "b75243f9-b3ba-4bb2-b1a7-7cfe4028f95e";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = ROLE_ID,
                    ConcurrencyStamp = ROLE_ID
                }, new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
            //create user
            var appUser = new ApiUser
            {
                Id = ADMIN_ID,
                Email = "primoadmin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Abdo",
                LastName = "Lukas",
                UserName = "primoadmin@gmail.com",
                NormalizedUserName = "PRIMOADMIN@GMAIL.COM"
            };

            //set user password
            PasswordHasher<ApiUser> ph = new PasswordHasher<ApiUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "P@ssword1");

            //seed user
            builder.Entity<ApiUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
            builder.ApplyConfiguration(new CountriesConfiguration());
            builder.ApplyConfiguration(new CurrenciesConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ServicesConfiguration());
            builder.ApplyConfiguration(new ShipmentsConfiguration());
            builder.ApplyConfiguration(new BonusConfiguration());
            builder.ApplyConfiguration(new WeeklyWorkSchedulesConfiguration());
            builder.ApplyConfiguration(new OrdersConfiguration());
            builder.ApplyConfiguration(new WarehouseCountingsConfiguration());
            builder.ApplyConfiguration(new ProductsConfiguration());
            builder.ApplyConfiguration(new MaterialsConfiguration());
            builder.ApplyConfiguration(new NonStandartWorksConfiguration());
        }
    }
}
