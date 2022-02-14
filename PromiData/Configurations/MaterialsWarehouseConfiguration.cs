using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;
using System;

namespace PromiData.Configurations.Entities
{
    public class MaterialsWarehouseConfiguration : IEntityTypeConfiguration<MaterialWarehouse>
    {
        public void Configure(EntityTypeBuilder<MaterialWarehouse> builder)
        {
            builder.HasData(
                new MaterialWarehouse
                {
                    Id = 1,
                    Title = "Fanera 3mm",
                    MeasuringUnit = "cm",
                    Quantity = 22500,
                    Info = "viena plokste 1,5x1,5m =22500",
                    DeliveryTime = 5,
                    UseDays = 40,
                    LastAdittion = DateTime.Now

                }
            );
        }
    }
}
