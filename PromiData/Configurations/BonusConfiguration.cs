﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;
using System;

namespace PromiData.Configurations.Entities
{

    public class BonusConfiguration : IEntityTypeConfiguration<Bonus>
    {
        public void Configure(EntityTypeBuilder<Bonus> builder)
        {
            var id = Guid.Parse("c9490c27-1b89-4e39-8f2e-99b48dcc709e");
            builder.HasData(
                new Bonus
                {
                    Id = 1,
                    UserId = id,
                    Quantity = 1000,
                    Accumulated = 100,
                    Bonusas = 600
                }
            );
        }
    }
}
