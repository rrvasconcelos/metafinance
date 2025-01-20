using FluentResults;
using MediatR;
using MetaFinance.Api.Common;
using MetaFinance.Application.Features.Categories.CreateCategory;

namespace MetaFinance.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Result<CreateCategoryCommandResponse>>();
    
    private static async Task<IResult> HandleAsync(IMediator mediator, CreateCategoryCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess
            ? TypedResults.Created($"/{result.Value?.Id}", result.Value)
            : TypedResults.BadRequest(result.Value);
    }
}