using MetaFinance.Domain.SharedKernel.Base;

namespace MetaFinance.Domain.Tests;

public class TestableAuditableEntity(string createdBy)
    : AuditableEntity<int>(createdBy)
{
    public void TestUpdateAudit(string modifiedBy)
    {
        UpdateAudit(modifiedBy);
    }
}

public class AuditableBehavior
{
    [Fact]
    public void Constructor_Should_ThrowException_WhenCreatedByIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new TestableAuditableEntity(""));
    }

    [Fact]
    public void Constructor_Should_ThrowException_WhenCreatedByIsNull()
    {
        Assert.Throws<ArgumentException>(() =>
            new TestableAuditableEntity(null));
    }

    [Fact]
    public void Constructor_Should_SetCreatedBy_WhenValid()
    {
        // Arrange
        const string createdBy = "test_user";

        // Act
        var entity = new TestableAuditableEntity(createdBy);

        // Assert
        Assert.Equal(createdBy, entity.CreatedBy);
    }

    [Fact]
    public void CreatedAt_Should_BeUtcNow_WhenCreatingNewEntity()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var entity = new TestableAuditableEntity("test_user");
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.True(entity.CreatedAt >= beforeCreate);
        Assert.True(entity.CreatedAt <= afterCreate);
    }

    [Fact]
    public void UpdateAudit_Should_ThrowException_WhenModifiedByIsEmpty()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            entity.TestUpdateAudit(""));
    }

    [Fact]
    public void UpdateAudit_Should_ThrowException_WhenModifiedByIsNull()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            entity.TestUpdateAudit(null));
    }

    [Fact]
    public void LastModifiedAt_Should_BeUpdated_WhenUpdatingEntity()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");
        var beforeUpdate = DateTime.Now;

        // Act
        entity.TestUpdateAudit("modified_user");
        var afterUpdate = DateTime.Now;

        // Assert
        Assert.NotNull(entity.LastModifiedAt);
        Assert.True(entity.LastModifiedAt >= beforeUpdate);
        Assert.True(entity.LastModifiedAt <= afterUpdate);
    }

    [Fact]
    public void UpdateAudit_Should_SetLastModifiedBy_WhenValid()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");
        const string modifiedBy = "modified_user";

        // Act
        entity.TestUpdateAudit(modifiedBy);

        // Assert
        Assert.Equal(modifiedBy, entity.LastModifiedBy);
    }

    [Fact]
    public void LastModifiedAt_Should_BeNull_WhenNotModified()
    {
        // Arrange & Act
        var entity = new TestableAuditableEntity("test_user");

        // Assert
        Assert.Null(entity.LastModifiedAt);
        Assert.Null(entity.LastModifiedBy);
    }
}

public class BaseEntityBehavior
{
    [Fact]
    public void Equals_Should_ReturnFalse_WhenComparingWithNull()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");

        // Act & Assert
        Assert.False(entity.Equals(null));
    }

    [Fact]
    public void GetHashCode_Should_ReturnZero_WhenIdIsNull()
    {
        // Arrange
        var entity = new TestableAuditableEntity("test_user");

        // Act & Assert
        Assert.Equal(0, entity.GetHashCode());
    }
}
