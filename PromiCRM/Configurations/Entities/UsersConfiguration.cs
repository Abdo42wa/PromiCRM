using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations.Entities
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var id = Guid.Parse("c9490c27-1b89-4e39-8f2e-99b48dcc709e");
            builder.HasData(
                new User
                {
                    Id = id,
                    Name = "Adminas",
                    Surname = "Admin",
                    Email = "abdo@gmail.com",
                    PhoneNumber = "860855183",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password1"),
                    TypeId = 1
                }
            );
        }
    }
}
