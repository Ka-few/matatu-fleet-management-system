using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Matatu.Application.DTOs.Auth;
using Matatu.Application.Interfaces;
using Matatu.Domain.Entities;
using Matatu.Domain.Enums;
using Matatu.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Matatu.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Pin, user.PinHash))
            {
                throw new Exception("Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id.ToString(),
                Token = token,
                FullName = user.FullName,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
             if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
            {
                throw new Exception("User already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PinHash = BCrypt.Net.BCrypt.HashPassword(request.Pin),
                Role = UserRole.Driver, // Default to Driver for now, should be configurable
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id.ToString(),
                Token = token,
                FullName = user.FullName,
                Role = user.Role.ToString()
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "super-secret-key-that-should-be-in-env-vars-12345";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
