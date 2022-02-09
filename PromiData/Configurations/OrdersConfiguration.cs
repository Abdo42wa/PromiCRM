using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;
using System;

namespace PromiData.Configurations.Entities
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var id = Guid.Parse("c9490c27-1b89-4e39-8f2e-99b48dcc709e");
            builder.HasData(
                new Order
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = id,
                    OrderNumber = 200,
                    Date = DateTime.Now,
                    Platforma = "yeee",
                    MoreInfo = "eeeee",
                    OrderType = "Standartinis",
                    Quantity = 2,
                    ProductCode = "8582262s",
                    ShipmentTypeId = 1,
                    CustomerId = 1,
                    Address = "Justiniskiu",
                    CountryId = 1,
                    ProductionTime = 1,
                    Comment = "great",
                    Device = "ira",
                    Price = 99.99,
                    Vat = 21.1,
                    Status = false,
                    OrderFinishDate = DateTime.Now
                }
            );
        }
    }
}
