using AutoMapper;
using DineHub.Domain.Entities;

namespace DineHub.Application.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
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