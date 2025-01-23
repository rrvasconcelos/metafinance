using FluentResults;
using Mapster;
using MediatR;
using MetaFinance.Application.Validation;
using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

namespace MetaFinance.Application.Features.Categories.UpdateCategory;

public record UpdateCategoryCommand(int Id, string Name, string? Description, CategoryType Type) 
    : IRequest<Result<UpdateCategoryCommandResponse>>, ICategoryCommand;
    
    
    public class UpdateCategoryCommandHandler(ICategoryUnitOfWork categoryUnitOfWork)
        : IRequestHandler<UpdateCategoryCommand, Result<UpdateCategoryCommandResponse>>
    {
        public async Task<Result<UpdateCategoryCommandResponse>> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await categoryUnitOfWork
                .Categories
                .GetByIdAsync(request.Id);

            if (category is null)
            {
                return Result.Fail(new CategoryNotFoundError(request.Id));
            }
            
            var categoryExisted = await categoryUnitOfWork
                .Categories
                .ExistsByNameAndDifferentIdAsync(request.Name, request.Id, cancellationToken);

            if (categoryExisted)
            {
                return Result.Fail(new CategoryAlreadyExistsError(request.Name));
            }

            category.Update(
                request.Name,
                request.Type,
                request.Description,
                "default_user");

            await categoryUnitOfWork.SaveChangesAsync(cancellationToken);

            var response = category.Adapt<UpdateCategoryCommandResponse>();

            return Result.Ok(response);
        }
    }