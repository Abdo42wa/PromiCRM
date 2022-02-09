using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;

namespace PromiData.Configurations.Entities
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
                },

                new Shipment
                {
                    Id = 2,
                    Type = "Paprastas",
                    Period = 2,
                    ShippingCost = 20.40,
                    ShippingNumber = 252,
                    ShipmentInfo = "atidaryk ta"
                }
            );
        }
    }
}
