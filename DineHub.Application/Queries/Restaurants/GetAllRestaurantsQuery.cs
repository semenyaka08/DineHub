using DineHub.Application.Common;
using DineHub.Application.Dtos.RestaurantDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQuery : IRequest<PageResult<GetRestaurantDto>>
{
    public string? SearchString { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}