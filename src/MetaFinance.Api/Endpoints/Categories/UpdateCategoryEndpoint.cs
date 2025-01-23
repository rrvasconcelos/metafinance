using FluentResults;
using MediatR;
using MetaFinance.Api.Common;
using MetaFinance.Api.Extensions;
using MetaFinance.Application.Features.Categories.UpdateCategory;
using Microsoft.AspNetCore.Mvc;

namespace MetaFinance.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Update a category")
            .WithDescription("Update a category")
            .WithOrder(2)
            .Produces<Result<UpdateCategoryCommandResponse>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator, 
        int id, 
        [FromBody] UpdateCategoryCommand command)
    {
        if(id != command.Id)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Title = "Invalid Id",
                Detail = "The id in the route and in the body must be the same",
                Status = 400
            });
        }
        
        var result = await mediator.Send(command);

        return result.ToResponse();
    }
}