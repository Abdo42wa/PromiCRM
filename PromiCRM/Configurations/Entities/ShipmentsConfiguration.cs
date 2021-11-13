using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class ShipmentsConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasData(
                new Shipment
                {
                    Id = 1,
                    Type = "Express",
                    Period = 2,
                    ShippingCost = 20.40,
                    ShippingNumber = 252,
                    ShipmentInfo = "atidaryk ta"
                }
            );
        }
    }
}
