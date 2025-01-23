using FluentResults;

namespace MetaFinance.Application.Validation;

public class CategoryNotFoundError(int categoryId)
    : Error($"A category with id '{categoryId}' was not found in the database");