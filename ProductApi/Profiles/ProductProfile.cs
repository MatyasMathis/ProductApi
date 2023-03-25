using AutoMapper;

namespace ProductApi.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile() {
            CreateMap<Models.Domain.Product,Models.DTOs.ProductDTO>().ReverseMap();
        }
    }
}
