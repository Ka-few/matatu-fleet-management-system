using System.ComponentModel.DataAnnotations;

namespace Matatu.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Pin { get; set; } = string.Empty;
    }

    public class RegisterRequestDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Pin { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
