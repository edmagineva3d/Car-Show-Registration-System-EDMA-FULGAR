namespace Application.Features.Registrations.CreateRegistration;

using Domain.Common;
using FluentValidation;

public sealed class CreateRegistrationValidator : AbstractValidator<CreateRegistrationCommand>
{
    public CreateRegistrationValidator()
    {
        RuleFor(x => x.ParticipantName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(150);
        RuleFor(x => x.ContactNumber).Cascade(CascadeMode.Stop).NotEmpty()
            .Matches(@"^09\d{9}$")
            .WithMessage("'Contact Number' must be a PH mobile number in the format 09XXXXXXXXX (11 digits).");
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(300);
        RuleFor(x => x.CarMakeModel).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PlateNumber).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(15);
        RuleFor(x => x.RegistrationType).IsInEnum();

        RuleFor(x => x.AmountPaid).GreaterThanOrEqualTo(0)
            .WithMessage("'Amount Paid' must be 0 or more.");

        RuleFor(x => x.AmountPaid)
            .LessThanOrEqualTo(x => RegistrationFees.For(x.RegistrationType))
            .WithMessage(x => $"'Amount Paid' cannot be more than the fee ({RegistrationFees.For(x.RegistrationType):N0}).")
            .When(x => Enum.IsDefined(x.RegistrationType) && x.AmountPaid >= 0);
    }
}