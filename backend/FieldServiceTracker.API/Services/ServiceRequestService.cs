using FieldServiceTracker.API.DTOs;
using FieldServiceTracker.API.Exceptions;
using FieldServiceTracker.API.Models;
using FieldServiceTracker.API.Repositories;

namespace FieldServiceTracker.API.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _repository;
        private readonly ILogger<ServiceRequestService> _logger;

        public ServiceRequestService(IServiceRequestRepository repository, ILogger<ServiceRequestService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<ServiceRequestResponseDto>> GetAllAsync(string? status, string? priority)
        {
            var serviceRequests = await _repository.GetAllAsync(status, priority);
            return serviceRequests.Select(MapToResponse).ToList();
        }

        public async Task<ServiceRequestResponseDto?> GetByIdAsync(int id)
        {
            var serviceRequest = await _repository.GetByIdAsync(id);

            if(serviceRequest == null)
            {
                _logger.LogWarning("Service request with ID {Id} not found", id);
                throw new NotFoundException($"Service request with ID {id} not found");
            }

            return MapToResponse(serviceRequest);
        }

        public async Task<ServiceRequestResponseDto> CreateAsync(CreateServiceRequestDto dto)
        {
            var ticketNumber = GenerateTicketNumber();

            var existing = await _repository.GetByTicketNumberAsync(ticketNumber);

            if (existing != null)
            {
                _logger.LogWarning("Duplicate ticket number generated: {TicketNumber}", ticketNumber);
                ticketNumber = GenerateTicketNumber();
            }

            var request = new ServiceRequest
            {
                TicketNumber = ticketNumber,
                CustomerName = dto.CustomerName.Trim(),
                Location = dto.Location.Trim(),
                IssueDescription = dto.IssueDescription.Trim(),
                Priority = dto.Priority,
                Status = "Open",
                AssignedTechnician = dto.AssignedTechnician?.Trim()
            };

            var created = await _repository.CreateAsync(request);

            await AddAuditLogAsync(created, "Created", $"Service request created with priority {created.Priority}");

            _logger.LogInformation("Created service request {TicketNumber}", created.TicketNumber);

            return MapToResponse(created);
        }

        public async Task<ServiceRequestResponseDto> UpdateAsync(int id, UpdateServiceRequestDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Service request with ID {Id} not found for update", id);
                throw new NotFoundException($"Service request with ID {id} not found");
            }

            existing.CustomerName = dto.CustomerName?.Trim() ?? existing.CustomerName;
            existing.Location = dto.Location?.Trim() ?? existing.Location;
            existing.IssueDescription = dto.IssueDescription?.Trim() ?? existing.IssueDescription;
            existing.Priority = dto.Priority ?? existing.Priority;
            existing.Status = dto.Status ?? existing.Status;
            existing.AssignedTechnician = dto.AssignedTechnician?.Trim() ?? existing.AssignedTechnician;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            
            await AddAuditLogAsync(existing, "Updated", $"Service request updated with priority {existing.Priority} and status {existing.Status}");

            _logger.LogInformation("Updated service request {TicketNumber}", existing.TicketNumber);
            
            return MapToResponse(existing);
        }

        public async Task<ServiceRequestResponseDto> PatchStatusAsync(int id, PatchStatusDto dto)
        {
            var request = await _repository.GetByIdAsync(id);

            if (request == null)
            {
                _logger.LogWarning("Service request with ID {Id} not found for status update", id);
                throw new NotFoundException($"Service request with ID {id} not found");
            }

            request.Status = dto.Status;
            request.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(request);

            await AddAuditLogAsync(request, "Status Updated", $"Service request status updated to {request.Status}");

            _logger.LogInformation("Updated status of service request {TicketNumber} to {Status}", request.TicketNumber, dto.Status);

            return MapToResponse(request);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Service request with ID {Id} not found for deletion", id);
                throw new NotFoundException($"Service request with ID {id} not found");
            }

            await AddAuditLogAsync(existing, "Deleted", $"Service request with ticket number {existing.TicketNumber} deleted");

            await _repository.DeleteAsync(existing);
            _logger.LogInformation("Deleted service request {TicketNumber}", existing.TicketNumber);
        }

        public async Task AddAuditLogAsync(ServiceRequest serviceRequest, string action, string details)
        {
            var auditLog = new ServiceRequestAuditLog
            {
                ServiceRequestId = serviceRequest.Id,
                TicketNumber = serviceRequest.TicketNumber,
                Action = action,
                Details = details,
                CreatedAt = DateTime.UtcNow
            };
             
            await _repository.AddAuditLogAsync(auditLog);
        }
            
        private static string GenerateTicketNumber()
        {
            return $"FST-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
        }

        private static ServiceRequestResponseDto MapToResponse(ServiceRequest request)
        {
            return new ServiceRequestResponseDto
            {
                Id = request.Id,
                TicketNumber = request.TicketNumber,
                CustomerName = request.CustomerName,
                Location = request.Location,
                IssueDescription = request.IssueDescription,
                Priority = request.Priority,
                Status = request.Status,
                AssignedTechnician = request.AssignedTechnician,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            };
        }
    }
}
