using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiCore.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseContext _database;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrdersRepository(DatabaseContext database, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = database;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Employee made orders through whole month
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserMadeServicesDTO>> GetEmployeeOrders()
        {
            DateTime today = DateTime.Now;
            DateTime monthBefore = today.AddDays(-30);
            var users = await _unitOfWork.Users.GetAll();
            var usersDTOs = _mapper.Map<IList<UserDTO>>(users);
            var userServices = await _database.UserServices.
                Include(x => x.Order).
                Where(x => x.CompletionDate.Date > monthBefore.Date).
                Where(x => x.OrderService.ServiceId == 7).
                GroupBy(x => x.UserId, x => x.Order.Quantity,
                (userId, quantity) => new UserMadeServicesDTO
                {
                    UserId = userId,
                    Quantity = quantity.Sum()
                }).ToListAsync();
            foreach (UserMadeServicesDTO service in userServices)
            {
                service.User = usersDTOs.SingleOrDefault(x => x.Id == service.UserId);
            }
            return userServices;
        }

        /// <summary>
        /// getting all completed orders in past Month(30 days).
        /// </summary>
        /// <returns></returns>
        public async Task<IList<OrderDTO>> GetMonthOrders()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime today = DateTime.Now;
            DateTime monthBefore = today.AddDays(-30);
            //group by completion date. so for example each order of 2022/01/29 will be counted seper
            var orders = await _database.Orders.Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Date > monthBefore.Date).
                Where(o => o.OrderType != "Ne-standartinis").
                GroupBy(o => o.CompletionDate.Value.Date).Select(x => new OrderDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    CompletionDate = x.Key,
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return orders;
        }
        /// <summary>
        /// Daugiausia nepagamintu produktu. Not completed standart orders
        /// </summary>
        /// <returns></returns>
        public async Task<IList<OrderDTO>> GetMostUncompletedOrdersProducts()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false)
                .Where(o => o.OrderType == "Standartinis")
                .GroupBy(o => new { o.ProductCode, o.Product.ImagePath })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Quantity = o.Sum(o => o.Quantity),
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return orders;
        }
        /// <summary>
        ///  Nauji uzsakyti gaminiai (main dashboard). today made orders (no  completed).
        /// </summary>
        /// <returns></returns>
        public async Task<OrderDTO> GetNewOrders()
        {
            var today = DateTime.Today;
            var order = await _database.Orders.Where(o => o.Status == false)
                .Where(o => (o.Date.Date) == today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return order;
        }
        /// <summary>
        /// Getting non standart order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Order> GetNonStandartOrderById(int id)
        {
            var order = await _database.Orders.Where(o => o.Id == id).
                Include(o => o.User).
                Include(o => o.Shipment).
                Include(o => o.Customer).
                Include(o => o.Country).
                Include(o => o.OrderServices).
                ThenInclude(p => p.Service).
                OrderByDescending(o => o.OrderFinishDate).
                AsNoTracking().
                FirstOrDefaultAsync();
            return order;
        }
        /// <summary>
        /// not-standart orders for clients. NOT FINISHED
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Order>> GetNonStandartOrders()
        {
            var orders = await _database.Orders.Where(o => o.OrderType == "Ne-standartinis").
               Include(o => o.User).
               Include(o => o.Shipment).
               Include(o => o.Customer).
               Include(o => o.Country).
               Include(o => o.OrderServices).
               Include(o => o.UserServices).
               OrderByDescending(o => o.OrderFinishDate).
               AsNoTracking().
               ToListAsync();
            return orders;
        }
        /// not-standart orders for clients. NOT FINISHED
        public async Task<IList<Order>> GetNonStandartOrdersForClients()
        {
            var orders = await _database.Orders.
               Include(o => o.Customer).
               Include(o => o.User).
               Include(o => o.OrderServices).
               Where(o => o.OrderType == "Ne-standartinis").
               Where(o => o.Status == false).
               ToListAsync();
            return orders;
        }
        // get standart & warehouse order by id
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _database.Orders.Where(o => o.Id == id).
              Include(o => o.User).
              Include(o => o.Shipment).
              Include(o => o.Customer).
              Include(o => o.Country).
              Include(o => o.Product).
              ThenInclude(o => o.OrderServices).
              ThenInclude(o => o.Service).
              AsNoTracking().
              FirstOrDefaultAsync();
            return order;
        }
        //getting standart & warehouse orders
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _database.Orders.Where(o => o.OrderType != "Ne-standartinis").
               Include(o => o.User).
               Include(o => o.Shipment).
               Include(o => o.Customer).
               Include(o => o.Country).
               Include(o => o.UserServices).
               Include(o => o.Product).
               ThenInclude(o => o.OrderServices).
               AsNoTracking().
               ToListAsync();
            return orders;
        }
        // getting all completed orders. thats completed orders in past 5 weeks
        public async Task<IOrderedEnumerable<OrderDTO>> GetOrdersByWeeks()
        {
            var startDate = DateTime.Now.AddDays(-1 * 365);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var currentCulture = CultureInfo.CurrentCulture;
            DateTime today = DateTime.Now;
            DateTime fiveWeeksBefore = today.AddDays(-36);
            var orders = _database.Orders.AsEnumerable().Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Date > fiveWeeksBefore.Date).
                Where(o => o.OrderType != "Ne-standartinis").
                GroupBy(o => cal.GetWeekOfYear(o.CompletionDate.Value.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)).
                Select(x => new OrderDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    CompletionDate = x.Max(x => x.CompletionDate.Value.Date),
                    WeekNumber = x.Key
                }).OrderBy(o => o.WeekNumber);
            return orders;
        }
        //get orders that orderFinishDate is today, or it should have been made yesterday or ..
        public async Task<OrderDTO> GetOrdersNecessaryToMake()
        {
            var today = DateTime.Today;
            var order = await _database.Orders.Where(o => o.Status == false)
                .Where(o => (o.OrderFinishDate.Date) <= today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return order;
        }

        public async Task<int> GetOrderWithBiggestOrderNumber()
        {
            var order = await _database.Orders.OrderByDescending(x => x.OrderNumber).FirstOrDefaultAsync();
            if (order == null)
                return 0;
            else
                return order.OrderNumber;
        }
        // Laukiantys gaminiai. all orders that are not completed
        public async Task<OrderDTO> GetPendingProducts()
        {
            var order = await _database.Orders.Where(x => x.Status == false)
                .GroupBy(x => new { x.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return order;
        }
        // Get newest 10 orders that were made. Ordering by date. Getting only "Sandelis" "Standartinis" orders
        public async Task<IList<Order>> GetRecentOrders()
        {
            var orders = await _database.Orders.Include(o => o.User).Include(o => o.Product)
                .Where(o => o.Status == true)
                .Where(o => o.OrderType != "Ne-standartinis")
                .OrderByDescending(o => o.CompletionDate)
                .Take(10).ToListAsync();
            return orders;
        }
        //Rekomenduojama gaminti
        public async Task<IList<OrderDTO>> GetRecommendedForProductionOrders()
        {
            var orders = await _database.Orders
                .Where(o => o.Status == false)
                .GroupBy(o => new { o.Quantity, o.ProductCode })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    Quantity = o.Sum(x => x.Quantity)
                }).ToListAsync();
            return orders;
        }
        // Siandien pagaminta (main dashboard).
        public async Task<OrderDTO> GetTodayMadeProducts()
        {
            var today = DateTime.Today;
            var order = await _database.Orders.Where(o => o.Status == true)
                .Where(o => (o.CompletionDate.Value.Date) == today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return order;
        }
        //Noot finished express orders
        public async Task<IList<OrderDTO>> GetUncompletedExpressOrders()
        {
            var orders = await _database.Orders.Include(o => o.Product).
                Where(o => o.ShipmentTypeId == 1).
                Where(o => o.Status == false).
                Select(o => new OrderDTO
                {
                    OrderFinishDate = o.OrderFinishDate,
                    OrderNumber = o.OrderNumber,
                    Quantity = o.Quantity,
                    ProductCode = o.ProductCode,
                    ImagePath = o.ImagePath == null ? o.Product.ImagePath : o.ImagePath,
                    Platforma = o.Platforma
                }).OrderByDescending(o => o.OrderFinishDate).ToListAsync();
            return orders;
        }

        public async Task<IList<OrderDTO>> GetUncompletedOrdersForWarehouse()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false).Where(o => o.OrderType == "Sandelis")
                .GroupBy(x => new { x.ProductCode, x.Product.ImagePath })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Quantity = o.Sum(o => o.Quantity),
                    /*OrderFinishDate = o.Max(o => o.OrderFinishDate),*/
                    MinOrderFinishDate = o.Min(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return orders;
        }
        //Getting planned time. How much of services are done how much have to make in total
        public async Task<IList<WorkTimeDTO>> GetUncompletedOrdersTime()
        {
            var orders = await _database.Orders.
                Include(o => o.Product).
                ThenInclude(u => u.OrderServices).
                Where(o => o.Status == false).
                Select(o => new WorkTimeDTO
                {
                    LaserTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 1) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 1).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 1) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 1).TimeConsumption * o.Quantity : 0,
                    MilingTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 2) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 2).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 2) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 2).TimeConsumption * o.Quantity : 0,
                    PaintingTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 3) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 3).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 3) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 3).TimeConsumption * o.Quantity : 0,
                    GrindingTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 4) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 4).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 4) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 4).TimeConsumption * o.Quantity : 0,
                    BondingTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 5) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 5).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 5) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 5).TimeConsumption * o.Quantity : 0,
                    CollectionTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 6) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 6).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 6) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 6).TimeConsumption * o.Quantity : 0,
                    PackingTime = o.Product != null ?
                    o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 7) != null ? o.Product.OrderServices.SingleOrDefault(p => p.ServiceId == 7).TimeConsumption * o.Quantity : 0
                    : o.OrderServices.SingleOrDefault(p => p.ServiceId == 7) != null ? o.OrderServices.SingleOrDefault(p => p.ServiceId == 7).TimeConsumption * o.Quantity : 0,
                    DoneLaserTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 1) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 1).OrderService.TimeConsumption * o.Quantity : 0,
                    DoneMilingTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 2) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 2).OrderService.TimeConsumption * o.Quantity : 0,
                    DonePaintingTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 3) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 3).OrderService.TimeConsumption * o.Quantity : 0,
                    DoneGrindingTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 4) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 4).OrderService.TimeConsumption * o.Quantity : 0,
                    DoneBondingTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 5) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 5).OrderService.TimeConsumption * o.Quantity : 0,
                    DoneCollectionTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 6) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 6).OrderService.TimeConsumption * o.Quantity : 0,
                    DonePackingTime = o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 7) != null ? o.UserServices.SingleOrDefault(p => p.OrderService.ServiceId == 7).OrderService.TimeConsumption * o.Quantity : 0,
                }).ToListAsync();
            return orders;
        }
        // Neisiustu siuntiniu lentele
        public async Task<IList<OrderDTO>> GetUnsendedOrders()
        {
            var orders = await _database.Orders
                .Where(o => o.Status == true)
                .Where(o => o.ShippingNumber == null)
                .GroupBy(o => new { o.Quantity, o.ProductCode, o.OrderFinishDate, o.OrderNumber })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    Quantity = o.Key.Quantity,
                    OrderNumber = o.Key.OrderNumber,
                    OrderFinishDate = o.Key.OrderFinishDate
                }).ToListAsync();
            return orders;
        }


        //REPORTS - ATASKAITOS

        public async Task<IList<LastMonthSoldOrderDTO>> GetLastMonthSoldProducts()
        {
            //Getting only standart. select products with same code. and get prices
            var today = DateTime.Now;
            var lastMonth = today.AddMonths(-1).Date;
            var standartOrders = await _database.Orders.
                Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Year == lastMonth.Year).
                Where(o => o.CompletionDate.Value.Month == lastMonth.Month).
                Where(o => o.OrderType == "Standartinis").
                GroupBy(o => new { o.ProductCode, o.Product.ImagePath, o.Product.Name }).
                Select(o => new LastMonthSoldOrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Price = (int)o.Sum(o => o.Price * o.Quantity),
                    Quantity = o.Sum(o => o.Quantity),
                    Name =o.Key.Name
                }).ToListAsync();
            return standartOrders;
        }
        public async Task<IList<LastMonthSoldOrderDTO>> GetLastMonthNonStandartSoldProducts()
        {
            //Getting only non-standart. select products with same code. and get prices
            //getting by customers. 
            var today = DateTime.Now;
            var lastMonth = today.AddMonths(-1).Date;
            var nonStandartOrders = await _database.Orders.
                Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Year == lastMonth.Year).
                Where(o => o.CompletionDate.Value.Month == lastMonth.Month).
                Where(o => o.OrderType == "Ne-standartinis").
                GroupBy(o => new { o.Status, o.Customer.Name, o.Customer.LastName}).
                Select(o => new LastMonthSoldOrderDTO
                {
                    Price = (int)o.Sum(o => o.Price * o.Quantity),
                    Quantity = o.Sum(o => o.Quantity),
                    CustomerName = o.Key.Name,
                    CustomerLastName = o.Key.LastName
                }).ToListAsync();
            return nonStandartOrders;
        }
        //most popular products in selected period of time
        public async Task<IList<OrderDTO>> GetUncompletedOrdersByPlatforms()
        {
            //grouping all orders by platforma and orderFinish date so if platforma and date are same
            //it will group it to one then ordering from lowest date to highest.
            //becouse we need first to show those that are more late
            var orders = await _database.Orders.
                Where(o => o.Status == false).
                Where(o => o.OrderType != "Sandelis").
                GroupBy(o => new { o.Platforma, o.OrderFinishDate.Date }).
                Select(o => new OrderDTO
                {
                    Platforma = o.Key.Platforma,
                    Quantity = o.Sum(o => o.Quantity),
                    Price = (int)o.Sum(o => o.Quantity * o.Price),
                    OrderFinishDate = o.Key.Date
                }).
                OrderBy(o => o.OrderFinishDate).
                ToListAsync();
            return orders;
        }

        //getting orders grouped by platforms in specified period of time(between to dates)
        public async Task<IList<OrderDTO>> GetCompletedPlatformsOrdersByTime(DateTime dateFrom, DateTime dateTo)
        {
            var orders = await _database.Orders.
                Where(o => o.Status == true).
                Where(o => o.OrderFinishDate >= dateFrom).
                Where(o => o.OrderFinishDate <= dateTo).
                Where(o => o.OrderType != "Sandelis").
                GroupBy(o => new { o.Platforma }).
                Select(o => new OrderDTO
                {
                    Platforma = o.Key.Platforma,
                    Quantity = o.Sum(o => o.Quantity),
                    Price = (int)o.Sum(o => o.Quantity * o.Price)
                }).
                OrderByDescending(o => o.Price).
                ToListAsync();
            return orders;
        }

        //Ataskaita pagal pasirinkta salis ir laikotarpi)
        public async Task<IList<OrderDTO>> GetOrdersByTimeAndCountry(DateTime dateFrom, DateTime dateTo)
        {
            var orders = await _database.Orders.
                Where(o => o.Status == true).
                Where(o => o.OrderFinishDate >= dateFrom).
                Where(o => o.OrderFinishDate <= dateTo).
                GroupBy(o => new { o.CountryName, o.ProductCode }).
                Select(o => new OrderDTO
                {
                    CountryName = o.Key.CountryName,
                    Quantity = o.Sum(o => o.Quantity),
                    Price = (int)o.Sum(o => o.Quantity * o.Price),
                    ProductCode = o.Key.ProductCode
                }).
                OrderByDescending(o => o.Price).
                ToListAsync();
            return orders;
        }


        //Populiariausiu prekiu ataskaita per pasirinkta laikas
        public async Task<IList<OrderDTO>> GetPopularProductByTime(DateTime dateFrom, DateTime dateTo)
        {
            var orders = await _database.Orders.
                Where(o => o.Status == true).
                Where(o => o.OrderFinishDate >= dateFrom).
                Where(o => o.OrderFinishDate <= dateTo).
                GroupBy(o => new { o.ProductCode }).
                Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity),
                    Price = (int)o.Sum(o => o.Quantity * o.Price),
                    ProductCode = o.Key.ProductCode
                }).
                OrderByDescending(o => o.Price).
                ToListAsync();
            return orders;
        }

        //Deziu kiekis likes sandelyje ir vidutiniskai kiek laiko dar uzteks esancio kiekio
        public async Task<IList<ProductMaterialDTO>> GetAmountOfBox()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime today = DateTime.Now;
            DateTime monthBefore = today.AddDays(-30);
            //group by completion date. so for example each order of 2022/01/29 will be counted seper
            var ProductMaterials = await _database.ProductMaterials.Where(o => o.MaterialWarehouse.Title == "Deze").
                Where(o => o.RegisterDate.Date > monthBefore.Date).
                GroupBy(o => o.RegisterDate.Date).Select(x => new ProductMaterialDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    RegisterDate = x.Key,
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return ProductMaterials;
        }

        /*Task<IList<ProductMaterialDTO>> IOrdersRepository.GetAmountOfBox()
        {
            throw new NotImplementedException();
        }*/
    }
}
