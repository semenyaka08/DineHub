using AutoMapper;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands.Dishes;

public class UpdateDishCommandHandler(ILogger<UpdateDishCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating dish with id: {dishId} from restaurant with id: {restaurantId}", request.DishId, request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(z=>z.Id == request.DishId);

        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        mapper.Map(request, dish);
        await restaurantRepository.SaveChangesAsync();
    }
}