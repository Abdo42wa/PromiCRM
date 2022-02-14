using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;
using System;

namespace PromiData.Configurations.Entities
{
    public class WarehouseCountingsConfiguration : IEntityTypeConfiguration<WarehouseCounting>
    {
        public void Configure(EntityTypeBuilder<WarehouseCounting> builder)
        {
            builder.HasData(
                new WarehouseCounting
                {
                    Id = 1,
                    QuantityProductWarehouse = 2,
                    LastTimeChanging = DateTime.Now,
                    ProductCode = "8582262s",
                    OrderId = 1
                }
            );
        }
    }
}
