using FluentResults;
using Mapster;
using MediatR;
using MetaFinance.Domain.Financial.Interfaces.Repositories;

namespace MetaFinance.Application.Features.Categories.GetCategories;

public class GetCategoriesQuery : IRequest<Result<IEnumerable<GetCategoriesQueryResponse>>>
{
}

public class GetCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<GetCategoriesQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetCategoriesQueryResponse>>> Handle(GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        var response = result.Adapt<IEnumerable<GetCategoriesQueryResponse>>();

        return Result.Ok(response);
    }
}