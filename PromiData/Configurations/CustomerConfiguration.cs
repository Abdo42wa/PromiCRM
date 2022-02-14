using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromiData.Models;

namespace PromiData.Configurations.Entities
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Jonas",
                    LastName = "Vaiciulis",
                    Email = "jonasv@gmail.com",
                    PhoneNumber = "860855183",
                    CompanyName = "telia"
                }
            );
        }
    }
}
