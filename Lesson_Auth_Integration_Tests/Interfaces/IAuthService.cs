<<<<<<< HEAD
﻿using Lesson_Auth_Integration_Tests.DTOs;
=======
﻿// ====================================================================================================
// Interfaces/IAuthService.cs
// ====================================================================================================
using Lesson_Auth_Integration_Tests.DTOs;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}
