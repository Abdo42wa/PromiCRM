using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Bonus> Bonus { get;  }
        IGenericRepository<Country> Countries { get;  }
        IGenericRepository<Currency> Currencies { get; }
        IGenericRepository<Customer> Customers { get;  }
        IGenericRepository<Material> Materials { get;  }
        IGenericRepository<NonStandardWork> NonStandardWorks { get;  }
        IGenericRepository<Order> Orders { get;  }
        IGenericRepository<Product> Products { get;  }
        IGenericRepository<Service> Services { get;  }
        IGenericRepository<Shipment> Shipments { get;  }
        IGenericRepository<WarehouseCounting> WarehouseCountings { get; }
        IGenericRepository<WeeklyWorkSchedule> WeeklyWorkSchedules { get;  }

        Task Save();
    }
}
