using FieldServiceTracker.API.Models;

namespace FieldServiceTracker.API.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<List<ServiceRequest>> GetAllAsync(string? status, string? priority);
        Task<ServiceRequest?> GetByIdAsync(int id);
        Task<ServiceRequest> GetByTicketNumberAsync(string ticketNumber);
        Task<ServiceRequest> CreateAsync(ServiceRequest serviceRequest);
        Task UpdateAsync(ServiceRequest serviceRequest);
        Task DeleteAsync(ServiceRequest serviceRequest);
        Task AddAuditLogAsync(ServiceRequestAuditLog auditLog);
    }
}
