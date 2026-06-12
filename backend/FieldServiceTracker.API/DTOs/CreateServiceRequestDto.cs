namespace FieldServiceTracker.API.DTOs
{
    public class CreateServiceRequestDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
        public string? AssignedTechnician { get; set; }
    }
}
