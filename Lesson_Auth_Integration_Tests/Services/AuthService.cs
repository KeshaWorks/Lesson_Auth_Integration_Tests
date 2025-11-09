using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Interfaces;
using Lesson_Auth_Integration_Tests.Models;
using Lesson_Auth_Integration_Tests.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthRepository _authRepository;
    public AuthService(
        UserManager<IdentityUser> userManager,
        IAuthRepository authRepository)
    {
        _userManager = userManager;
        _authRepository = authRepository;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return result;

        await _authRepository.AssignUserRoleAsync(user.Id, "User");
        return result;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        return await _authRepository.GetJwtTokenAsync(request.Email, request.Password);
    }
}
