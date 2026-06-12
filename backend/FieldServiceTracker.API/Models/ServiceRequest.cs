namespace FieldServiceTracker.API.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
        public string? AssignedTechnician { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
