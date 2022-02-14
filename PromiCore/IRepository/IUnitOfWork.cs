using PromiData.Models;
using System;
using System.Threading.Tasks;

namespace PromiCore.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<UserType> UserTypes { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<SalesChannel> SalesChannels { get; }
        IGenericRepository<Bonus> Bonus { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<MaterialWarehouse> MaterialsWarehouse { get; }
        IGenericRepository<ProductMaterial> ProductMaterials { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<RecentWork> RecentWorks { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<OrderService> OrderServices { get; }
        IGenericRepository<UserService> UserServices { get; }
        IGenericRepository<Shipment> Shipments { get; }
        IGenericRepository<WarehouseCounting> WarehouseCountings { get; }
        IGenericRepository<WeeklyWorkSchedule> WeeklyWorkSchedules { get; }
        Task Save();
    }
}
