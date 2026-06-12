using FieldServiceTracker.API.DTOs;
using FluentValidation;

namespace FieldServiceTracker.API.Validators
{
    public class PatchStatusValidator : AbstractValidator<PatchStatusDto>
    {
        private readonly string[] _validStatuses = ["Open", "In Progress", "Resolved", "Closed"];

        public PatchStatusValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => _validStatuses.Contains(s, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Status must be one of the following: {string.Join(", ", _validStatuses)}");
        }
    }
}