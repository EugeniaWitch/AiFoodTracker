using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.DTOs.Auth;
using server.Models;
using server.Models.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace server.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLower();
        var name = request.Name.Trim();

        var userExists = await _context.Users.AnyAsync(u => u.Email == email);
        if (userExists)
        {
            throw new InvalidOperationException("User with this email already exists");
        }
        
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = name,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Sex = UserSex.NotSpecified,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var settings = new UserSettings
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UseStorage = true,
            UseCycleMode = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var nutritionGoal = new NutritionGoal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Users.Add(user);
        _context.UserProfiles.Add(profile);
        _context.UserSettings.Add(settings);
        _context.NutritionGoals.Add(nutritionGoal);

        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(user);

        return new AuthResponse
        {
            Token = token,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var email = request.Email.Trim().ToLower();
        
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (isPasswordValid!)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var token = GenerateJwtToken(user);
        
        return new AuthResponse
        {
            Token = token,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        
        var secretKey = jwtSettings["SecretKey"] ?? throw new
            InvalidOperationException("JWT SecretKey is not configured");

        var issuer = jwtSettings["Issuer"] ?? throw new
            InvalidOperationException("JWT Issuer is not configured");

        var audience = jwtSettings["Audience"] ?? throw new
            InvalidOperationException("JWT Audience is not configured");

        var expiresInMinutes = int.Parse(jwtSettings["ExpiresInMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Role,user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer : issuer,
            audience : audience,
            claims : claims,
            expires : DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials : credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}