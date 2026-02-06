using System.Threading.Tasks;
using Matatu.Application.DTOs.Auth;
using Matatu.Domain.Entities;

namespace Matatu.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}
