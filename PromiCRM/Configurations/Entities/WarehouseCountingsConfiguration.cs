using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
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
                    Photo = "https://www.apple.com/ac/structured-data/images/open_graph_logo.png?201809270954",
                    LastTimeChanging = DateTime.Now,
                    OrderId = 1
                }
            );
        }
    }
}
