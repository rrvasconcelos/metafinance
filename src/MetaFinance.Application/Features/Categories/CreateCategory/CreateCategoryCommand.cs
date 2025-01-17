using FluentResults;
using MediatR;
using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommand: IRequest<Result<CreateCategoryCommandResponse>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; private set; } = null!;
    public CategoryType Type { get; private set; }
    public bool IsActive { get; private set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    public Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}