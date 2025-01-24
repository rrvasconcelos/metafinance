using static MetaFinance.Domain.Tests.CategoryTests.Factory;

namespace MetaFinance.Domain.Tests;

public class AuditableBehavior
{
    [Fact]
    public void CreatedAt_Should_BeUtcNow_WhenCreatingNewCategory()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var category = CreateValidCategory();
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.True(category.CreatedAt >= beforeCreate);
        Assert.True(category.CreatedAt <= afterCreate);
    }

    [Fact]
    public void LastModifiedAt_Should_BeUpdated_WhenUpdatingCategory()
    {
        // Arrange
        var category = CreateValidCategory();
        var beforeUpdate = DateTime.Now;

        // Act
        category.Update("New Name", null, null, "modified_user");
        var afterUpdate = DateTime.Now;

        // Assert
        Assert.NotNull(category.LastModifiedAt);
        Assert.True(category.LastModifiedAt >= beforeUpdate);
        Assert.True(category.LastModifiedAt <= afterUpdate);
    }
}

public class BaseEntityBehavior
{
    [Fact]
    public void Equals_Should_ReturnFalse_WhenComparingWithNull()
    {
        // Arrange
        var category = CreateValidCategory();

        // Act & Assert
        Assert.False(category.Equals(null));
    }

    [Fact]
    public void GetHashCode_Should_ReturnZero_WhenIdIsNull()
    {
        // Arrange
        var category = CreateValidCategory();

        // Act & Assert
        Assert.Equal(0, category.GetHashCode());
    }

    [Fact]
    public void Properties_Should_BeImmutable()
    {
        // Arrange
        var category = CreateValidCategory();

        // Assert
        var nameProperty = typeof(Category).GetProperty(nameof(Category.Name));
        var typeProperty = typeof(Category).GetProperty(nameof(Category.Type));
        var descriptionProperty = typeof(Category).GetProperty(nameof(Category.Description));
        var isActiveProperty = typeof(Category).GetProperty(nameof(Category.IsActive));

        Assert.False(nameProperty?.SetMethod?.IsPublic);
        Assert.False(typeProperty?.SetMethod?.IsPublic);
        Assert.False(descriptionProperty?.SetMethod?.IsPublic);
        Assert.False(isActiveProperty?.SetMethod?.IsPublic);
    }
}