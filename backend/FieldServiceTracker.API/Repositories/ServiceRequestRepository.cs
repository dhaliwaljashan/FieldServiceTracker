using FieldServiceTracker.API.Data;
using FieldServiceTracker.API.Models;
using Microsoft.EntityFrameworkCore;


namespace FieldServiceTracker.API.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly AppDbContext _context;

        public ServiceRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllAsync(string? status, string? priority)
        {
            var query = _context.ServiceRequests.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(sr => sr.Status == status);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(sr => sr.Priority == priority);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<ServiceRequest?> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests.FindAsync(id);
        }

        public async Task<ServiceRequest?> GetByTicketNumberAsync(string ticketNumber)
        {
            return await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.TicketNumber == ticketNumber);
        }

        public async Task<ServiceRequest> CreateAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task UpdateAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Update(serviceRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();
        }

        public async Task AddAuditLogAsync(ServiceRequestAuditLog auditLog)
        {
            _context.ServiceRequestAuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}
