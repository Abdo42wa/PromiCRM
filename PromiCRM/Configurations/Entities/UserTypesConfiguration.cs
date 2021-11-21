using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class UserTypesConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasData(
                new UserType
                {
                    Id = 1,
                    Title = "ADMINISTRATOR"
                },
                new UserType
                {
                    Id = 2,
                    Title = "USER"
                },
                new UserType
                {
                    Id = 3,
                    Title = "GAMYBA"
                },
                new UserType
                {
                    Id = 4,
                    Title = "VADYBA"
                }
            );
        }
    }
}
