namespace MetaFinance.Domain.Tests;

public class CategoryTests
{
    private const string DefaultUser = "test_user";

    public class Constructor
    {
        [Fact]
        public void Should_CreateValidCategory_WhenValidParametersProvided()
        {
            // Arrange
            const string name = "Test Category";
            const CategoryType type = CategoryType.Expense;
            const string description = "Test Description";

            // Act
            var category = new Category(name, type, description, DefaultUser);

            // Assert
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.Equal(description, category.Description);
            Assert.True(category.IsActive);
            Assert.Equal(DefaultUser, category.CreatedBy);
            Assert.NotEqual(default, category.CreatedAt);
        }

        [Fact]
        public void Should_CreateValidCategory_WithNullDescription()
        {
            // Arrange
            const string name = "Test Category";
            const CategoryType type = CategoryType.Expense;

            // Act
            var category = new Category(name, type, null, DefaultUser);

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
                new Category(invalidName, CategoryType.Expense, "Description", DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenNameIsNull()
        {
            // Arrange
            string? nullName = null;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(nullName!, CategoryType.Expense, "Description", DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenNameExceeds100Characters()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category(longName, CategoryType.Expense, "Description", DefaultUser));

            Assert.Equal("Category name cannot exceed 100 characters.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenTypeIsInvalid()
        {
            // Arrange
            const CategoryType invalidType = (CategoryType)999;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                new Category("Test", invalidType, "Description", DefaultUser));

            Assert.Equal("Invalid category type.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ThrowArgumentException_WhenCreatedByIsEmptyOrWhitespace(string invalidUser)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Category("Test", CategoryType.Expense, "Description", invalidUser));
        }

        [Fact]
        public void Should_ThrowArgumentException_WhenCreatedByIsNull()
        {
            // Arrange
            string? nullUser = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Category("Test", CategoryType.Expense, "Description", nullUser!));
        }
    }

    public class UpdateMethod
    {
        private readonly Category _category = new("Initial Name", CategoryType.Expense, "Initial Description",
            DefaultUser);

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
                _category.Update(invalidName, null, null, DefaultUser));

            Assert.Equal("Category name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Should_ThrowDomainException_WhenUpdatingWithInvalidType()
        {
            // Arrange
            const CategoryType invalidType = (CategoryType)999;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                _category.Update("Test", invalidType, null, DefaultUser));

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
    }

    public class ActivationMethods
    {
        private readonly Category _category = new("Test", CategoryType.Expense, "Description", DefaultUser);

        [Fact]
        public void Deactivate_Should_SetIsActiveToFalse()
        {
            // Act
            _category.Deactivate(DefaultUser);

            // Assert
            Assert.False(_category.IsActive);
            Assert.Equal(DefaultUser, _category.LastModifiedBy);
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
            _category.Deactivate(DefaultUser);

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
            _category.Deactivate(DefaultUser);
            var modifiedBy = "another_user";

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
}