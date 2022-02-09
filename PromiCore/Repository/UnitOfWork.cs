using PromiCore.IRepository;
using PromiCore.Repository;
using PromiData.Models;
using System;
using System.Threading.Tasks;

namespace PromiCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<UserType> _userTypes;
        private IGenericRepository<User> _users;
        private IGenericRepository<SalesChannel> _salesChannels;
        private IGenericRepository<Bonus> _bonus;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<MaterialWarehouse> _materialsWarehouse;
        private IGenericRepository<ProductMaterial> _productMaterials;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<Product> _products;
        private IGenericRepository<RecentWork> _recentWorks;
        private IGenericRepository<Service> _services;
        private IGenericRepository<OrderService> _orderServices;
        private IGenericRepository<UserService> _userServices;
        private IGenericRepository<Shipment> _shipments;
        private IGenericRepository<WarehouseCounting> _warehouseCountings;
        private IGenericRepository<WeeklyWorkSchedule> _weeklyWorkSchedules;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<UserType> UserTypes => _userTypes ??= new GenericRepository<UserType>(_context);
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<SalesChannel> SalesChannels => _salesChannels ??= new GenericRepository<SalesChannel>(_context);
        public IGenericRepository<Bonus> Bonus => _bonus ??= new GenericRepository<Bonus>(_context);
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);
        public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<MaterialWarehouse> MaterialsWarehouse => _materialsWarehouse ??= new GenericRepository<MaterialWarehouse>(_context);
        public IGenericRepository<ProductMaterial> ProductMaterials => _productMaterials ??= new GenericRepository<ProductMaterial>(_context);
        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);
        public IGenericRepository<RecentWork> RecentWorks => _recentWorks ??= new GenericRepository<RecentWork>(_context);
        public IGenericRepository<Service> Services => _services ??= new GenericRepository<Service>(_context);
        public IGenericRepository<OrderService> OrderServices => _orderServices ??= new GenericRepository<OrderService>(_context);
        public IGenericRepository<UserService> UserServices => _userServices ??= new GenericRepository<UserService>(_context);
        public IGenericRepository<Shipment> Shipments => _shipments ??= new GenericRepository<Shipment>(_context);

        public IGenericRepository<WarehouseCounting> WarehouseCountings => _warehouseCountings ??= new GenericRepository<WarehouseCounting>(_context);

        public IGenericRepository<WeeklyWorkSchedule> WeeklyWorkSchedules => _weeklyWorkSchedules ??= new GenericRepository<WeeklyWorkSchedule>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
