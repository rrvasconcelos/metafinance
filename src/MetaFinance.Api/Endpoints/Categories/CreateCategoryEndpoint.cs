using FluentResults;
using MediatR;
using MetaFinance.Api.Common;
using MetaFinance.Api.Extensions;
using MetaFinance.Application.Features.Categories.CreateCategory;

namespace MetaFinance.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Create a new category")
            .WithDescription("Create a new category")
            .WithOrder(1)
            .Produces<Result<CreateCategoryCommandResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator, CreateCategoryCommand command)
    {
        var result = await mediator.Send(command);

        return !result.IsSuccess ? 
            result.ToResponse() : 
            result.ToCreatedResponse($"/categories/{result.Value.Id}");
    }
}