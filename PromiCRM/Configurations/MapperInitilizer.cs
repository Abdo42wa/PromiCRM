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
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserType, UserTypeDTO>();

            CreateMap<SalesChannel, SalesChannelDTO>();
            CreateMap<SalesChannel, CreateSalesChannelDTO>();
            CreateMap<SalesChannel, UpdateSalesChannelDTO>();

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

            CreateMap<MaterialWarehouse, MaterialWarehouseDTO>().ReverseMap();
            CreateMap<MaterialWarehouse, CreateMaterialWarehouseDTO>().ReverseMap();
            CreateMap<MaterialWarehouse, UpdateMaterialWarehouseDTO>().ReverseMap();
            CreateMap<MaterialWarehouse, MaterialWarehouseForm>().ReverseMap();
            

            CreateMap<ProductMaterial, ProductMaterialDTO>().ReverseMap();
            CreateMap<ProductMaterial, CreateProductMaterialDTO>().ReverseMap();
            CreateMap<ProductMaterial, UpdateProductMaterialDTO>().ReverseMap();

/*            CreateMap<Order, NonStandardWorkDTO>().ReverseMap();
            CreateMap<Order, CreateNonStandardWorksDTO>().ReverseMap();
            CreateMap<Order, UpdateNonStandardWorksDTO>().ReverseMap();*/

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            /*CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<Service, CreateServiceDTO>().ReverseMap();
            CreateMap<Service, UpdateServiceDTO>().ReverseMap();*/
            CreateMap<SalesChannel, SalesChannelDTO>().ReverseMap();
            CreateMap<SalesChannel, CreateSalesChannelDTO>().ReverseMap();
            CreateMap<SalesChannel, UpdateSalesChannelDTO>().ReverseMap();


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
