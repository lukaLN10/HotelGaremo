using FluentValidation;

namespace HotelGaremo.Application.Features.Users.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(3)
            .Matches(@"^[a-zA-Zა-ჰ]+$").WithMessage("Name must contain only letters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(5)
            .Matches(@"^[a-zA-Zა-ჰ]+$").WithMessage("Last name must contain only letters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^5\d{8}$")
            .WithMessage("Phone number must be a valid Georgian mobile number.");

        RuleFor(x => x.DateOfBirth)
            .Must(x => x <= DateTime.Today.AddYears(-18))
            .WithMessage("User must be at least 18 years old.");
    }
}
