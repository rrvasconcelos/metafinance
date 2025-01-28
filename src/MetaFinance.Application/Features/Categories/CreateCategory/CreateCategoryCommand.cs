﻿using FluentResults;
using Mapster;
using MediatR;
using MetaFinance.Application.Validation;
using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string? Description, CategoryType Type) 
    : IRequest<Result<CreateCategoryCommandResponse>>, ICategoryCommand;

public class CreateCategoryCommandHandler(ICategoryUnitOfWork categoryUnitOfWork)
    : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    public async Task<Result<CreateCategoryCommandResponse>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryExist = await categoryUnitOfWork.Categories
            .ExistsByNameAsync(request.Name, cancellationToken);

        if (categoryExist)
        {
            return Result.Fail(new CategoryAlreadyExistsError(request.Name));
        }
        
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