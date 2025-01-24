using System.Reflection;

namespace MetaFinance.Domain.Tests;

public class CategoryTests
{
    private static class TestConstants
    {
        public const string DefaultUser = "test_user";
        public const string ValidName = "Test Category";
        public const string ValidDescription = "Test Description";
        public const CategoryType DefaultType = CategoryType.Expense;
    }

    private static Category CreateValidCategory(
        string name = TestConstants.ValidName,
        CategoryType type = TestConstants.DefaultType,
        string? description = TestConstants.ValidDescription,
        string createdBy = TestConstants.DefaultUser)
    {
        return new Category(name, type, description, createdBy);
    }

    public class Constructor
    {
        [Fact]
        public void Should_CreateValidCategory_WhenValidParametersProvided()
        {
            // Arrange
            const string name = TestConstants.ValidName;
            const CategoryType type = TestConstants.DefaultType;
            const string description = TestConstants.ValidDescription;

            // Act
            var category = new Category(name, type, description, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.Equal(description, category.Description);
            Assert.True(category.IsActive);
            Assert.Equal(TestConstants.DefaultUser, category.CreatedBy);
            Assert.NotEqual(default, category.CreatedAt);
        }

        [Fact]
        public void Should_CreateValidCategory_WithNullDescription()
        {
            // Arrange
            const string name = TestConstants.ValidName;
            const CategoryType type = TestConstants.DefaultType;

            // Act
            var category = new Category(name, type, null, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.Null(category.Description);
            Assert.True(category.IsActive);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ThrowDomainException_WhenNameIsEmptyOrWhitespace(string invalidName)
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(invalidName, TestConstants.DefaultType, TestConstants.ValidDescription,
                    TestConstants.DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenNameIsNull()
        {
            // Arrange
            string? nullName = null;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(nullName!, TestConstants.DefaultType, TestConstants.ValidDescription,
                    TestConstants.DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenNameExceeds100Characters()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(longName, TestConstants.DefaultType, TestConstants.ValidDescription,
                    TestConstants.DefaultUser));

            Assert.Equal("Category name cannot exceed 100 characters.", exception.Message);
        }

        [Fact]
        public void Should_AcceptName_WithExactly100Characters()
        {
            // Arrange
            var name100Chars = new string('a', 100);

            // Act
            var category = new Category(name100Chars, TestConstants.DefaultType, null, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(100, category.Name.Length);
        }

        [Theory]
        [InlineData(CategoryType.Income)]
        [InlineData(CategoryType.Expense)]
        public void Should_AcceptValidCategoryTypes(CategoryType validType)
        {
            // Act
            var category = new Category(TestConstants.ValidName, validType, null, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(validType, category.Type);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenTypeIsInvalid()
        {
            // Arrange
            const CategoryType invalidType = (CategoryType)999;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(TestConstants.ValidName, invalidType, TestConstants.ValidDescription,
                    TestConstants.DefaultUser));

            Assert.Equal("Invalid category type.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ThrowArgumentException_WhenCreatedByIsEmptyOrWhitespace(string invalidUser)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Category(TestConstants.ValidName, TestConstants.DefaultType, TestConstants.ValidDescription,
                    invalidUser));
        }

        [Fact]
        public void Should_ThrowArgumentException_WhenCreatedByIsNull()
        {
            // Arrange
            string? nullUser = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Category(TestConstants.ValidName, TestConstants.DefaultType, TestConstants.ValidDescription,
                    nullUser!));
        }

        [Theory]
        [InlineData(" Test ", "Test")]
        [InlineData("  Test  ", "Test")]
        public void Should_TrimName_WhenCreatingOrUpdatingCategory(string inputName, string expectedName)
        {
            // Arrange & Act
            var category = new Category(inputName, TestConstants.DefaultType, null, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(expectedName, category.Name);
        }

        [Theory]
        [InlineData(" Description ", "Description")]
        [InlineData("  Description  ", "Description")]
        public void Should_TrimDescription_WhenCreatingCategory(string inputDescription, string expectedDescription)
        {
            // Arrange & Act
            var category = new Category(TestConstants.ValidName, TestConstants.DefaultType, inputDescription,
                TestConstants.DefaultUser);

            // Assert
            Assert.Equal(expectedDescription, category.Description);
        }

        [Fact]
        public void Constructor_Should_ValidateNameBeforeType()
        {
            // Arrange
            string invalidName = "";
            var invalidType = (CategoryType)999;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(invalidName, invalidType, TestConstants.ValidDescription, TestConstants.DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void DefaultConstructor_Should_SetDefaultUser()
        {
            // Arrange & Act
            var category = CreateCategoryUsingDefaultConstructor();

            // Assert
            Assert.Equal("default_user", category.CreatedBy);
        }

        [Fact]
        public void DefaultConstructor_Should_InitializePropertiesCorrectly()
        {
            // Arrange & Act
            var category = CreateCategoryUsingDefaultConstructor();

            // Assert
            Assert.Equal(string.Empty, category.Name);
            Assert.Equal(default(CategoryType), category.Type);
            Assert.Null(category.Description);
            Assert.False(category.IsActive);
            Assert.Equal("default_user", category.CreatedBy);
        }
    }

    public class UpdateMethod
    {
        private readonly Category _category = new(TestConstants.ValidName, TestConstants.DefaultType,
            TestConstants.ValidDescription,
            TestConstants.DefaultUser);

        [Fact]
        public void Should_UpdateAllProperties_WhenValidParametersProvided()
        {
            // Arrange
            const string newName = "Updated Name";
            const CategoryType newType = CategoryType.Income;
            const string newDescription = "Updated Description";
            const string modifiedBy = "modified_user";

            // Act
            _category.Update(newName, newType, newDescription, modifiedBy);

            // Assert
            Assert.Equal(newName, _category.Name);
            Assert.Equal(newType, _category.Type);
            Assert.Equal(newDescription, _category.Description);
            Assert.Equal(modifiedBy, _category.LastModifiedBy);
            Assert.NotNull(_category.LastModifiedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ThrowDomainException_WhenUpdatingWithEmptyOrWhitespaceName(string invalidName)
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                _category.Update(invalidName, null, null, TestConstants.DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenUpdatingWithInvalidType()
        {
            // Arrange
            const CategoryType invalidType = (CategoryType)999;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                _category.Update("Test", invalidType, null, TestConstants.DefaultUser));

            Assert.Equal("Invalid category type.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ThrowArgumentException_WhenModifiedByIsEmptyOrWhitespace(string invalidUser)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _category.Update("New Name", CategoryType.Income, "New Description", invalidUser));
        }

        [Fact]
        public void Should_ThrowArgumentException_WhenModifiedByIsNull()
        {
            // Arrange
            string? nullUser = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _category.Update("New Name", CategoryType.Income, "New Description", nullUser!));
        }

        [Fact]
        public void Update_Should_NotModifyProperties_WhenNoChangesProvided()
        {
            // Arrange
            var category = CreateValidCategory();
            var originalName = category.Name;
            var originalType = category.Type;
            var originalDescription = category.Description;

            // Act
            category.Update(null, null, null, "modified_user");

            // Assert
            Assert.Equal(originalName, category.Name);
            Assert.Equal(originalType, category.Type);
            Assert.Equal(originalDescription, category.Description);
            Assert.NotNull(category.LastModifiedAt);
            Assert.Equal("modified_user", category.LastModifiedBy);
        }

        [Fact]
        public void Update_Should_OnlyUpdateDescription_WhenOnlyDescriptionProvided()
        {
            // Arrange
            var category = CreateValidCategory();
            var originalName = category.Name;
            var originalType = category.Type;
            const string newDescription = "New Description";

            // Act
            category.Update(null, null, newDescription, "modified_user");

            // Assert
            Assert.Equal(originalName, category.Name);
            Assert.Equal(originalType, category.Type);
            Assert.Equal(newDescription, category.Description);
            Assert.NotNull(category.LastModifiedAt);
            Assert.Equal("modified_user", category.LastModifiedBy);
        }

        [Theory]
        [InlineData(" New Description ", "New Description")]
        [InlineData("  New Description  ", "New Description")]
        public void Should_TrimDescription_WhenUpdatingCategory(string inputDescription, string expectedDescription)
        {
            // Arrange
            var category = CreateValidCategory();

            // Act
            category.Update(null, null, inputDescription, TestConstants.DefaultUser);

            // Assert
            Assert.Equal(expectedDescription, category.Description);
        }

        [Fact]
        public void Update_Should_ClearDescription_WhenEmptyStringProvided()
        {
            // Arrange
            var category = CreateValidCategory();

            // Act
            category.Update(null, null, "", "modified_user");

            // Assert
            Assert.Equal("", category.Description);
        }

        [Fact]
        public void Update_Should_ValidateNameBeforeType()
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                _category.Update("", (CategoryType)999, null, TestConstants.DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }
    }

    public class ActivationMethods
    {
        private readonly Category _category = CreateValidCategory();

        [Fact]
        public void Deactivate_Should_SetIsActiveToFalse()
        {
            // Act
            _category.Deactivate(TestConstants.DefaultUser);

            // Assert
            Assert.False(_category.IsActive);
            Assert.Equal(TestConstants.DefaultUser, _category.LastModifiedBy);
            Assert.NotNull(_category.LastModifiedAt);
        }

        [Fact]
        public void Deactivate_Should_UpdateAuditInformation()
        {
            // Arrange
            const string modifiedBy = "another_user";

            // Act
            _category.Deactivate(modifiedBy);

            // Assert
            Assert.Equal(modifiedBy, _category.LastModifiedBy);
            Assert.NotNull(_category.LastModifiedAt);
        }

        [Fact]
        public void Activate_Should_SetIsActiveToTrue()
        {
            // Arrange
            _category.Deactivate(TestConstants.DefaultUser);

            // Act
            _category.Activate("new_user");

            // Assert
            Assert.True(_category.IsActive);
            Assert.Equal("new_user", _category.LastModifiedBy);
            Assert.NotNull(_category.LastModifiedAt);
        }

        [Fact]
        public void Activate_Should_UpdateAuditInformation()
        {
            // Arrange
            _category.Deactivate(TestConstants.DefaultUser);
            const string modifiedBy = "another_user";

            // Act
            _category.Activate(modifiedBy);

            // Assert
            Assert.Equal(modifiedBy, _category.LastModifiedBy);
            Assert.NotNull(_category.LastModifiedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Deactivate_Should_ThrowArgumentException_WhenModifiedByIsEmptyOrWhitespace(string invalidUser)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _category.Deactivate(invalidUser));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Activate_Should_ThrowArgumentException_WhenModifiedByIsEmptyOrWhitespace(string invalidUser)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _category.Activate(invalidUser));
        }
    }

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

    private static Category CreateCategoryUsingDefaultConstructor()
    {
        var constructor = typeof(Category)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

        return (Category)constructor!.Invoke(null);
    }
}