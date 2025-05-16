using AutoMapper;
using DollarProject.Dto;
using DollarProject.Models;

namespace DollarProject.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.FirstName + " " + src.Seller.LastName))
                .ForMember(dest => dest.IsVerifiedSeller, opt => opt.MapFrom(src => src.Seller.IsVerifiedSeller));
        }
    }
}
