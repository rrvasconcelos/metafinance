using FluentResults;

namespace MetaFinance.Application.Validation;

public class CategoryAlreadyExistsError(string categoryName)
    : Error($"A category with name '{categoryName}' already exists in the database");