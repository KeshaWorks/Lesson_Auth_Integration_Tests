using Lesson_Auth_Integration_Tests.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}
