using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<UserType> UserTypes{ get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<SalesChannel> SalesChannels { get; }
        IGenericRepository<Bonus> Bonus { get;  }
        IGenericRepository<Country> Countries { get;  }
        IGenericRepository<Currency> Currencies { get; }
        IGenericRepository<Customer> Customers { get;  }
        IGenericRepository<MaterialWarehouse> MaterialsWarehouse { get; }
        IGenericRepository<ProductMaterial> ProductMaterials { get;  }
        IGenericRepository<Order> Orders { get;  }
        IGenericRepository<Product> Products { get;  }
/*        IGenericRepository<Service> Services { get;  }*/
        IGenericRepository<Shipment> Shipments { get;  }
        IGenericRepository<WarehouseCounting> WarehouseCountings { get; }
        IGenericRepository<WeeklyWorkSchedule> WeeklyWorkSchedules { get;  }
        Task Save();
    }
}
