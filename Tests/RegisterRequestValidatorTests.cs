using FluentValidation.TestHelper;
using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Validators;

namespace Tests;

public class RegisterRequestValidatorTests
{
    private readonly RegisterRequestValidator _validator = new();

    [Fact]
    public void Validate_ShouldHaveError_WhenEmailIsEmpty()
    {
        var model = new RegisterRequest("", "pass", "Full Name");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPasswordTooShort()
    {
        var model = new RegisterRequest("test@example.com", "123", "Full Name");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validate_ShouldBeValid_WhenAllPropertiesValid()
    {
        var model = new RegisterRequest("test@example.com", "pass123", "Full Name");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldntBeValid_WhenEmailNotValid()
    {
        var model = new RegisterRequest("test-email", "pass123", "Full Name");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("Email");
    }
}
