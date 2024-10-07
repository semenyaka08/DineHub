using AutoMapper;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Domain.Entities;
using DineHub.Domain.Exceptions;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Restaurants;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, GetRestaurantDto>
{
    public async Task<GetRestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant with id: {Id}", request.Id);

        var restaurant = await restaurantRepository.GetById(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantDto = mapper.Map<GetRestaurantDto>(restaurant);

        return restaurantDto;
    }
}