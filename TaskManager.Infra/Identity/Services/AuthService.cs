using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Core.Entities;
using TaskManager.Core.JWT;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infra.Identity.Services;

public class AuthService(UserManager<TaskUser> userManager,
                   SignInManager<TaskUser> signInManager,
                   JwtSettings jwtSettings) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings;
    private readonly UserManager<TaskUser> _userManager = userManager;
    private readonly SignInManager<TaskUser> _signInManager = signInManager;

    public async Task<string> RegisterAsync(RegisterRequest model)
    {
        var user = new TaskUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.ToDictionary(
                e => e.Code,
                e => new[] { e.Description }));
        }

        return "User registered successfully";
    }

    public async Task<AuthTokenResponse> LoginAsync(LoginRequest model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
            throw new UnauthorizedException("Invalid credentials");

        var user = await _userManager.FindByEmailAsync(model.Email)
                   ?? throw new NotFoundException("User", model.Email);
        var token = GenerateJwtToken(user);

        return new AuthTokenResponse { Token = token };
    }

    public string GenerateJwtToken(TaskUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<TaskUser> GetUserByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID cannot be empty.", nameof(id));

        var user = await _userManager.FindByIdAsync(id);
        return user ?? throw new NotFoundException("User", id);
    }

    public async Task<TaskUser> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        var user = await _userManager.FindByEmailAsync(email);
        return user ?? throw new NotFoundException("User", email);
    }

    public async Task<TaskUser> GetUserByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        var user = await _userManager.FindByNameAsync(username);
        return user ?? throw new NotFoundException("User", username);
    }

    public async Task<bool> UpdateUserAsync(UpdateUserRequest model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "ApplicationUser cannot be null.");

        var user = await GetUserByIdAsync(model.Id) ?? throw new NotFoundException("User", model.Id);

        user.Email = model.Email;
        user.UserName = model.FullName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.ToDictionary(
                e => e.Code,
                e => new[] { e.Description }));
        }
        return result.Succeeded;
    }

    public async Task<bool> DeactivateUserAsync(string id)
    {
        var user = await GetUserByIdAsync(id) ?? throw new NotFoundException("User", id);
        user.IsActive = false;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.ToDictionary(
                e => e.Code,
                e => new[] { e.Description }));
        }
        return result.Succeeded;
    }
}
