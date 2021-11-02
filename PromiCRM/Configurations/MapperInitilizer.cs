using AutoMapper;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Configurations
{
    // inherit from Profile which is basically automapper
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDTO>().ReverseMap();

            CreateMap<Bonus, BonusDTO>().ReverseMap();
            CreateMap<Bonus, CreateBonusDTO>().ReverseMap();
            CreateMap<Bonus, UpdateBonusDTO>().ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<Currency, CreateCurrencyDTO>().ReverseMap();
            CreateMap<Currency, UpdateCurrencyDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();

            CreateMap<Material, MaterialDTO>().ReverseMap();
            CreateMap<Material, CreateMaterialDTO>().ReverseMap();
            CreateMap<Material, UpdateMaterialDTO>().ReverseMap();

            CreateMap<NonStandardWorks, NonStandardWorksDTO>().ReverseMap();
            CreateMap<NonStandardWorks, CreateNonStandardWorksDTO>().ReverseMap();
            CreateMap<NonStandardWorks, UpdateNonStandardWorksDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            CreateMap<Service, ServicesDTO>().ReverseMap();
            CreateMap<Service, CreateServicesDTO>().ReverseMap();
            CreateMap<Service, UpdateServicesDTO>().ReverseMap();

            CreateMap<Shipment, ShipmentDTO>().ReverseMap();
            CreateMap<Shipment, CreateShipmentDTO>().ReverseMap();
            CreateMap<Shipment, UpdateShipmentDTO>().ReverseMap();

            CreateMap<WarehouseCounting, WarehouseCountingDTO>().ReverseMap();
            CreateMap<WarehouseCounting, CreateWarehouseCountingDTO>().ReverseMap();
            CreateMap<WarehouseCounting, UpdateWarehouseCountingDTO>().ReverseMap();

            CreateMap<WeeklyWorkSchedule, WeeklyWorkScheduleDTO>().ReverseMap();
            CreateMap<WeeklyWorkSchedule, CreateWeeklyWorkScheduleDTO>().ReverseMap();
            CreateMap<WeeklyWorkSchedule, UpdateWeeklyWorkScheduleDTO>().ReverseMap();


        }
    }
}
