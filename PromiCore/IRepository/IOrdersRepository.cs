using PromiCore.ModelsDTO;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiCore.IRepository
{
    public interface IOrdersRepository
    {
        //STANDART & WAREHOUSE ORDERS
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        // Daugiausia nepagamintu produktu. Not completed standart orders
        Task<IList<OrderDTO>> GetMostUncompletedOrdersProducts();
        Task<IList<OrderDTO>> GetUncompletedOrdersForWarehouse();
        // getting all completed orders in past Month(30 days).
        Task<IList<OrderDTO>> GetMonthOrders();
        // getting all completed orders. thats completed orders in past 5 weeks
        Task<IOrderedEnumerable<OrderDTO>> GetOrdersByWeeks();
        // Get newest 10 orders that were made. Ordering by date. Getting only "Sandelis" "Standartinis" orders
        Task<IList<Order>> GetRecentOrders();

        //BOTH
        Task<int> GetOrderWithBiggestOrderNumber();
        //laukiantys gaminiai. orders that are not completed
        Task<OrderDTO> GetPendingProducts();
        //get orders that orderFinishDate is today, or it should have been made yesterday or ..
        Task<OrderDTO> GetOrdersNecessaryToMake();
        // Siandien pagaminta (main dashboard).
        Task<OrderDTO> GetTodayMadeProducts();
        //Nauji uzsakyti gaminiai (main dashboard). today made orders (no  completed).
        Task<OrderDTO> GetNewOrders();
        //Noot finished express orders
        Task<IList<OrderDTO>> GetUncompletedExpressOrders();
        //Getting planned time. How much of services are done how much have to make in total
        Task<IList<WorkTimeDTO>> GetUncompletedOrdersTime();
        // Neisiustu siuntiniu lentele
        Task<IList<OrderDTO>> GetUnsendedOrders();
        //Rekomenduojama gaminti
        Task<IList<OrderDTO>> GetRecommendedForProductionOrders();
        //Employee made orders through whole month
        Task<IList<UserMadeServicesDTO>> GetEmployeeOrders();



        //NON-STANDART
        Task<IList<Order>> GetNonStandartOrders();
        Task<Order> GetNonStandartOrderById(int id);
        //Non-standart orders for clients. not finished
        Task<IList<Order>> GetNonStandartOrdersForClients();


        //ATASKAITOS - REPORTS
        Task<IList<LastMonthSoldOrderDTO>> GetLastMonthSoldProducts();
        Task<IList<LastMonthSoldOrderDTO>> GetLastMonthNonStandartSoldProducts();
        //most popular products in selected period of time
        Task<IList<OrderDTO>> GetUncompletedOrdersByPlatforms();

        Task<IList<OrderDTO>> GetCompletedPlatformsOrdersByTime(DateTime dateFrom, DateTime dateTo);
        Task<IList<OrderDTO>> GetOrdersByTimeAndCountry(DateTime dateFrom, DateTime dateTo);
        Task<IList<OrderDTO>> GetPopularProductByTime(DateTime dateFrom, DateTime dateTo);
        Task<IList<ProductMaterialDTO>> GetAmountOfBox();
    }
}
