using FluentResults;
using MediatR;
using MetaFinance.Domain.Financial.Interfaces.Repositories;

namespace MetaFinance.Application.Features.Categories.GetCategories;

public class GetCategoriesQuery: IRequest<Result<IEnumerable<GetCategoriesQueryResponse>>>
{
    
}

public class GetCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<GetCategoriesQueryResponse>>>
{

    public async Task<Result<IEnumerable<GetCategoriesQueryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        return Result.Ok(result.Select(x => new GetCategoriesQueryResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Type = x.Type,
            IsActive = x.IsActive
        }));
    }
}