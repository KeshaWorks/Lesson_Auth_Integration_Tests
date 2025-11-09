using Microsoft.AspNetCore.Identity;

public interface IAuthRepository
{
    Task<IdentityResult> AssignUserRoleAsync(string userId, string roleName);
    Task<string?> GetJwtTokenAsync(string email, string password);
}
