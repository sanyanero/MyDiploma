using AutoMapper;
using System;
using Test.MODELS.DTO;
using Test.MODELS.Entities;

namespace Test.MODELS.Profiles
{
    public class ProductProfile : Profile {
        public ProductProfile () {
            CreateMap<Product, ProductDTO>().MaxDepth(1);
            CreateMap<ProductDTO, Product>().ForMember(dest => dest.Date, opts => opts.UseValue(DateTimeOffset.UtcNow)).MaxDepth(1);
        }
    }
}