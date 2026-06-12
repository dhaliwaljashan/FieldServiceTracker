using FieldServiceTracker.API.DTOs;

namespace FieldServiceTracker.API.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
