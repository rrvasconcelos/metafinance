using FluentResults;
using Mapster;
using MediatR;
using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public record CreateCategoryCommand(string Name, string? Description, CategoryType Type) 
    : IRequest<Result<CreateCategoryCommandResponse>>;

public class CreateCategoryCommandHandler(ICategoryUnitOfWork categoryUnitOfWork)
    : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    public async Task<Result<CreateCategoryCommandResponse>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category(
            request.Name,
            request.Type,
            request.Description,
            "default_user");

        await categoryUnitOfWork.Categories.CreateAsync(category);
        await categoryUnitOfWork.SaveChangesAsync(cancellationToken);

        var response = category.Adapt<CreateCategoryCommandResponse>();

        return Result.Ok(response);
    }
}