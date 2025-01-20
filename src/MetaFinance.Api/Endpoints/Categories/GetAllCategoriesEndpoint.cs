using FluentResults;
using MediatR;
using MetaFinance.Api.Common;
using MetaFinance.Application.Features.Categories.GetCategories;
using Microsoft.AspNetCore.Mvc;

namespace MetaFinance.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(2)
            .Produces<Result<IEnumerable<GetCategoriesQueryResponse>>>();

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoriesQuery());
        
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.Value);
    }
}