using DineHub.Domain.Entities;
using DineHub.Domain.Enums;

namespace DineHub.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant,RecourseOperation operation);
}