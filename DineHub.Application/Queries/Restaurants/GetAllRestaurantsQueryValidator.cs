using DineHub.Application.Common;
using FluentValidation;

namespace DineHub.Application.Queries.Restaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowedPageSize = [5, 10, 15, 30];
    private readonly string[] _allowedSortItems = ["Name", "Rating", "Category"];
    private readonly SortOrder[] _allowedSortOrder = [SortOrder.Ascending, SortOrder.Descending];
    
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(z => z.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(z => z.PageSize)
            .Must(m=>_allowedPageSize.Contains(m))
            .WithMessage($"Page size must be one of the following values: [{string.Join(",", _allowedPageSize)}]");

        RuleFor(z => z.SortItem)
            .Must(z => _allowedSortItems.Contains(z))
            .WithMessage($"Sort item must be one of the following values: [{string.Join(",", _allowedSortItems)}]");

        RuleFor(z => z.SortOrder)
            .Must(z => _allowedSortOrder.Contains(z))
            .WithMessage($"Sort order must be one of the following values: [{string.Join(",", _allowedSortOrder)}]");
    }
}