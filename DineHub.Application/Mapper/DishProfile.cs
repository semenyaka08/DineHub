using AutoMapper;
using DineHub.Application.Commands.Dishes;
using DineHub.Domain.Entities;

namespace DineHub.Application.Mapper;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<CreateDishCommand, Dish>();
        
        CreateMap<Dish, Dtos.DishDtos.DishDto>();
    }
}