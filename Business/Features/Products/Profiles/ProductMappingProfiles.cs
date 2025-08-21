using AutoMapper;
using Business.Features.Products.Commands.Create;
using Business.Features.Products.Commands.Update;
using Business.Features.Products.Queries.GetById;
using Business.Features.Products.Queries.GetList;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Profiles
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            //   .ForMember(i => i.UnitPrice, opt => opt.MapFrom(dto => dto.Price)); // Özel konfigürasyondur. Entity'deki UnitPrice ile Dto'daki Price'ı mapler.
            CreateMap<Product, GetAllProductResponse>().ReverseMap();
            CreateMap<Product, GetByIdProductResponse>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap(); // Command ve Query için ayrı ayrı oluşturulur.
            CreateMap<Product, UpdateProductResponse>().ReverseMap();
        }
    }
}


// Profile -> İki nesne arasında mapleme yapılacak ama bu nesneleri bir tanımla. Hnagi nesne ile hangi nesne arasında ilişki var.

// Constructor içerisinde, bu profil kuralları tanımlanır.

// CreateMap<TSource, TDestination>(); şeklinde çalışır.

// TSource -> Verileri alacağımız yer.

// TDestination -> Alınan kaynaktan üretilecek class.

// ReverseMap(); -> Her iki yönde de mapleme işlemi gerçekleştirir.