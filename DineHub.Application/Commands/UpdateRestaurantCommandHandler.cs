using AutoMapper;
using DineHub.Application.Commands.Restaurants;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Commands;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetById(request.Id);

        if (restaurant == null)
            return false;

        mapper.Map(request, restaurant);
        return await restaurantRepository.SaveChanges();
    }
}