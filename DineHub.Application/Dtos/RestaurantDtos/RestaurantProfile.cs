using AutoMapper;
using DineHub.Domain.Entities;

namespace DineHub.Application.Dtos.RestaurantDtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(z => z.Address, opt =>
                opt.MapFrom(src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));
        
        CreateMap<Restaurant, GetRestaurantDto>()
            .ForMember(z => z.City, opt =>
                opt.MapFrom(src => src.Address!.City))
            .ForMember(z => z.Street, opt =>
                opt.MapFrom(src => src.Address!.Street))
            .ForMember(z => z.PostalCode, opt =>
                opt.MapFrom(src => src.Address!.PostalCode))
            .ForMember(z => z.Dishes, opt =>
                opt.MapFrom(src => src.Dishes));
    }
}