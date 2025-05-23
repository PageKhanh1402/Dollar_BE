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
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.IsVerifiedSeller, opt => opt.MapFrom(src => src.User.IsVerifiedSeller))
                .ForMember(dest => dest.SellerImageURL, opt => opt.MapFrom(src => src.User.ImageURL ?? "user-placeholder.png"))
                .ForMember(dest => dest.IsInWishlist, opt => opt.Ignore());
        }
    }
}
