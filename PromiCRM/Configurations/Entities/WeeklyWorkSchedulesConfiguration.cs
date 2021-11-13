using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class WeeklyWorkSchedulesConfiguration : IEntityTypeConfiguration<WeeklyWorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WeeklyWorkSchedule> builder)
        {
            var id = Guid.Parse("c9490c27-1b89-4e39-8f2e-99b48dcc709e");
            builder.HasData(
                new WeeklyWorkSchedule
                {
                    Id = 1,
                    UserId = id,
                    DarbasApibūdinimas = "yeee",
                    Atlikta = false
                }
            );
        }
    }
}
