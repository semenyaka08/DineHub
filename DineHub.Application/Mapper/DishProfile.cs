using AutoMapper;
using DineHub.Application.Commands.Dishes;
using DineHub.Application.Dtos.DishDtos;
using DineHub.Domain.Entities;

namespace DineHub.Application.Mapper;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<UpdateDishCommand, Dish>();
        
        CreateMap<CreateDishCommand, Dish>();
        
        CreateMap<Dish, DishDto>();
    }
}