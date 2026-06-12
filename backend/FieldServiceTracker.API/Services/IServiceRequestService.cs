using FieldServiceTracker.API.DTOs;

namespace FieldServiceTracker.API.Services
{
    public interface IServiceRequestService
    {
        Task<List<ServiceRequestResponseDto>> GetAllAsync(string? status, string? priority);
        Task<ServiceRequestResponseDto?> GetByIdAsync(int id);
        Task<ServiceRequestResponseDto> CreateAsync(CreateServiceRequestDto createDto);
        Task<ServiceRequestResponseDto> UpdateAsync(int id, UpdateServiceRequestDto updateDto);
        Task<ServiceRequestResponseDto> PatchStatusAsync(int id, PatchStatusDto patchDto);
        Task DeleteAsync(int id);
    }
}
