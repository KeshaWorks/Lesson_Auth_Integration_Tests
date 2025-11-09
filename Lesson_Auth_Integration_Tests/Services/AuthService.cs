<<<<<<< HEAD
﻿using Lesson_Auth_Integration_Tests.DTOs;
=======
﻿// ====================================================================================================
// Services/AuthService.cs
// ====================================================================================================
using Lesson_Auth_Integration_Tests.DTOs;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.Interfaces;
using Lesson_Auth_Integration_Tests.Models;
using Lesson_Auth_Integration_Tests.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Services;

<<<<<<< HEAD
=======
// SOLID: Single Responsibility - only authentication logic
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthRepository _authRepository;

<<<<<<< HEAD

=======
    // SOLID: Dependency Inversion - depends on abstractions
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
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

<<<<<<< HEAD

=======
        // SOLID: Open/Closed - we can extend role assignment without modifying core logic
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        await _authRepository.AssignUserRoleAsync(user.Id, "User");
        return result;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        return await _authRepository.GetJwtTokenAsync(request.Email, request.Password);
    }
}
