using AutoMapper;
using DineHub.Domain.Entities;

namespace DineHub.Application.Dtos.DishDtos;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDtos.DishDto>();
    }
}