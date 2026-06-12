namespace FieldServiceTracker.API.DTOs
{
    public class UpdateServiceRequestDto
    {
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string IssueDescription { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string? AssignedTechnician { get; set; }
    }
}
