using AutoMapper;
using DineHub.Application.Dtos.DishDtos;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Dishes;

public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IDishRepository dishRepository) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dish with id: {@Id} and restaurantId: {@restaurantId}", request.Id, request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        var dish = restaurant.Dishes.FirstOrDefault(z=>z.Id == request.Id);
        
        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());
        
        var dishDto = mapper.Map<DishDto>(dish);

        return dishDto;
    }
}