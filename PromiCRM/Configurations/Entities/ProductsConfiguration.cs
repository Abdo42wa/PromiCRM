using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    OrderId = 1,
                    Photo = "https://www.apple.com/ac/structured-data/images/open_graph_logo.png?201809270954",
                    Link = "sss",
                    Code = "8582262s",
                    Category = "Good",
                    Name = "Produktas",
                    LengthWithoutPackaging = 10.0,
                    WidthWithoutPackaging = 5.0,
                    HeightWithoutPackaging = 3.0,
                    WeightGross = 10.2,
                    PackagingBoxCode = "pspspsp",
                    PackingTime = 10.0,
                    ServiceId = 1
                }
            );
        }
    }
}
