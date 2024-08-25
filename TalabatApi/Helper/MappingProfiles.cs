using AutoMapper;
using Talabat.Core.Entities;
using TalabatApi.DTOs;

namespace TalabatApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p=>p.ProductType,s=>s.MapFrom(p=>p.ProductType.Name))
                .ForMember(p=>p.ProductBrand,s=>s.MapFrom(p=>p.ProductBrand.Name));
        }
    }
}
