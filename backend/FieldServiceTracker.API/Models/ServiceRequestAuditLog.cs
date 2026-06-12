namespace FieldServiceTracker.API.Models
{
    public class ServiceRequestAuditLog
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
