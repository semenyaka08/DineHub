using AutoMapper;
using DineHub.Application.Common;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Application.User;
using DineHub.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PageResult<GetRestaurantDto>>
{
    public async Task<PageResult<GetRestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants!");

        var (restaurants, itemsCount) = await restaurantRepository.GetAllMatchingRestaurantsAsync(request.SearchString, request.PageSize, request.PageNumber);

        var restaurantsDto = mapper.Map<List<GetRestaurantDto>>(restaurants);

        var pageResult = new PageResult<GetRestaurantDto>(restaurantsDto, itemsCount, request.PageSize, request.PageNumber);
        
        return pageResult;
    }
}