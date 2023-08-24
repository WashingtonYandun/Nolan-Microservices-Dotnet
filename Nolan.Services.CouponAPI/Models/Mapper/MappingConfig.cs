using AutoMapper;
using Nolan.Services.CouponAPI.Models.Dto;

namespace Nolan.Services.CouponAPI.Models.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
