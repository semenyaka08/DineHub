using AutoMapper;
using DineHub.Application.Dtos.DishDtos;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Dishes;

public class GetAllDishesQueryHandler(ILogger<GetAllDishesQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IDishRepository dishRepository) : IRequestHandler<GetAllDishesQuery, List<DishDto>>
{
    public async Task<List<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all dishes from restaurant with id: {@id}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dishes = restaurant.Dishes.ToList();

        var dishesDto = mapper.Map<List<DishDto>>(dishes);

        return dishesDto;
    }
}