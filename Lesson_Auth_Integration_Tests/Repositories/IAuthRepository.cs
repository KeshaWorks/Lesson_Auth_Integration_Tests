<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity;
=======
﻿// ====================================================================================================
// Repositories/IIAuthRepository.cs
// ====================================================================================================
using Microsoft.AspNetCore.Identity;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

namespace Lesson_Auth_Integration_Tests.Repositories;

public interface IAuthRepository
{
    Task<IdentityResult> AssignUserRoleAsync(string userId, string roleName);
    Task<string?> GetJwtTokenAsync(string email, string password);
}
