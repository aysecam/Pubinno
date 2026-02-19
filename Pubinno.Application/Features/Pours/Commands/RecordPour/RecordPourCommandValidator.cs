using FluentValidation;
using Pubinno.Domain.Constants;

namespace Pubinno.Application.Features.Pours.Commands.RecordPour
{
    public class RecordPourCommandValidator : AbstractValidator<RecordPourRequest>
    {
        public RecordPourCommandValidator()
        {
            RuleFor(x => x.EventId)
                .NotEmpty().WithMessage("EventId is required");

            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("DeviceId is required");

            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("LocationId is required")
                .Must(x => AllowedValues.LocationIds.Contains(x))
                .WithMessage("LocationId is not valid");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required")
                .Must(x => AllowedValues.ProductIds.Contains(x))
                .WithMessage("ProductId is not valid");

            RuleFor(x => x.VolumeMl)
                .Must(x => AllowedValues.VolumeMlAllowed.Contains(x))
                .WithMessage("VolumeMl is not valid");

            RuleFor(x => x.EndedAt)
                .GreaterThanOrEqualTo(x => x.StartedAt)
                .WithMessage("EndedAt must be greater than or equal to StartedAt");
        }
    }
}
