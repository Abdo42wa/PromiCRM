using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class NonStandartWorksConfiguration : IEntityTypeConfiguration<NonStandardWork>
    {
        public void Configure(EntityTypeBuilder<NonStandardWork> builder)
        {
            builder.HasData(
                new NonStandardWork
                {
                    Id = 1,
                    OrderNumber = 255,
                    Date = DateTime.Now,
                    OrderDeadline = DateTime.Now,
                    DaysUntilDeadline = 2,
                    CustomerId = 1,
                    Device = "Device",
                    PlannedProductionTime = 40,
                    Comment = "Komentaras",
                    MaterialId = 1,
                    Status = false
                }
            );
        }
    }
}
