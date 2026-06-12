using FieldServiceTracker.API.DTOs;
using FluentValidation;

namespace FieldServiceTracker.API.Validators
{
    public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequestDto>
    {
        private readonly string[] _validPriorities = ["Low", "Medium", "High", "Critical"];
        private readonly string[] _validStatuses = ["Open", "In Progress", "Resolved", "Closed"];

        public UpdateServiceRequestValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(150).WithMessage("Location cannot exceed 150 characters.");

            RuleFor(x => x.IssueDescription)
                .NotEmpty().WithMessage("Issue description is required.")
                .MaximumLength(1000).WithMessage("Issue description cannot exceed 1000 characters.");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required.")
                .Must(p => _validPriorities.Contains(p, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Priority must be one of the following: {string.Join(", ", _validPriorities)}");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => _validStatuses.Contains(s, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Status must be one of the following: {string.Join(", ", _validStatuses)}");

            RuleFor(x => x.AssignedTechnician)
                .MaximumLength(100).WithMessage("Assigned technician cannot exceed 100 characters.");
        }
    }
}