using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>() //Mapper.CreateMap<Src, Dest>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name)) //d= Dest, o = on , s=source 
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name)) 
                .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductUrlResolver>()) ;
        }
    }
}