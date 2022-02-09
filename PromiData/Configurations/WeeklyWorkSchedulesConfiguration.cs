using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;
using System;

namespace PromiData.Configurations.Entities
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
                    Description = "Supildyti frezavimo laiko lentele",
                    Done = false,
                    Date = DateTime.Now
                }
            );
        }
    }
}
