using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;

namespace PromiData.Configurations.Entities
{
    public class OrderServicesConfiguration : IEntityTypeConfiguration<OrderService>
    {
        public void Configure(EntityTypeBuilder<OrderService> builder)
        {
           /* builder.HasData(
                *//* new OrderService
                 {
                     Id = 1,
                     ProductId = 1,
                     ServiceId = 1
                 },
                 new OrderService
                 {
                     Id = 2,
                     ProductId = 1,
                     ServiceId = 2
                 },
                 new OrderService
                 {
                     Id = 3,
                     ProductId = 1,
                     ServiceId = 3
                 }*//*
                );*/
        }
    }
}
