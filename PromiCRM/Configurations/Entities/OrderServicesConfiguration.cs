using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class OrderServicesConfiguration : IEntityTypeConfiguration<OrderService>
    {
        public void Configure(EntityTypeBuilder<OrderService> builder)
        {
            builder.HasData(
                new OrderService
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
                }
                );
        }
    }
}
