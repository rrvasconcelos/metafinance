using FluentResults;
using MediatR;
using MetaFinance.Api.Common;
using MetaFinance.Api.Extensions;
using MetaFinance.Application.Features.Transactions.CreateTransaction;

namespace MetaFinance.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Create a new transaction")
            .WithDescription("Create a new transaction")
            .WithOrder(1)
            .Produces<Result<CreateTransactionCommandResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator, CreateTransactionCommand command)
    {
        var result = await mediator.Send(command);

        return !result.IsSuccess ? 
            result.ToResponse() : 
            result.ToCreatedResponse($"/transactions/{result.Value.Id}");
    }
}