using AutoMapper;
using DineHub.Domain.Entities;

namespace DineHub.Application.Dtos;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDto>();
    }
}