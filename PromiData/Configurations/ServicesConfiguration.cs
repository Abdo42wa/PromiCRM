using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;

namespace PromiData.Configurations.Entities
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(
                new Service
                {
                    Id = 1,
                    Name = "Lazeriavimas"
                },
                new Service
                {
                    Id = 2,
                    Name = "Frezavimas"
                },
                new Service
                {
                    Id = 3,
                    Name = "Dažymas"
                },
                new Service
                {
                    Id = 4,
                    Name = "Šlifavimas"
                },
                new Service
                {
                    Id = 5,
                    Name = "Suklijavimas"
                },
                new Service
                {
                    Id = 6,
                    Name = "Surinkimas"
                },
                new Service
                {
                    Id = 7,
                    Name = "Pakavimas"
                }
            );
        }
    }
}
