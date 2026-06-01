using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(3);


        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(5);


        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.DateOfBirth)
            .Must(x => x <= DateTime.Today.AddYears(-18))
            .WithMessage("User must be at least 18 years old.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^5\d{8}$")
            .WithMessage("Phone number must be a valid Georgian mobile number.");
    }
}

