using PromiCRM.IRepository;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Bonus> _bonus;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Currency> _currencies;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Material> _materials;
        private IGenericRepository<NonStandardWorks> _nonStandardWorks;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Services> _services;
        private IGenericRepository<Shipment> _shipments;
        private IGenericRepository<WarehouseCounting> _warehouseCountings;
        private IGenericRepository<WeeklyWorkSchedule> _weeklyWorkSchedules;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<Bonus> Bonus => _bonus ??= new GenericRepository<Bonus>(_context);

        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Currency> Currencies => _currencies ??= new GenericRepository<Currency>(_context);

        public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);

        public IGenericRepository<Material> Materials => _materials ??= new GenericRepository<Material>(_context);

        public IGenericRepository<NonStandardWorks> NonStandardWorks => _nonStandardWorks ??= new GenericRepository<NonStandardWorks>(_context);

        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);

        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Services> Services => _services ??= new GenericRepository<Services>(_context);

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
