using DineHub.Application.Common;
using DineHub.Application.Dtos.RestaurantDtos;
using DineHub.Domain.Enums;
using MediatR;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQuery : IRequest<PageResult<GetRestaurantDto>>
{
    public string? SearchString { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public SortOrder SortOrder { get; set; } = SortOrder.Descending;

    public string SortItem { get; set; } = "Rating";
}