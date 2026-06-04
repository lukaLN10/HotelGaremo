using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.VerifyUser;

public class VerifyUserValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("ემაილი სავალდებულოა.")
            .EmailAddress().WithMessage("ემაილი არასწორი ფორმატისაა.");

        RuleFor(x => x.VerificationCode)
            .NotEmpty().WithMessage("ვერიფიკაციის კოდი სავალდებულოა.");
    }
}
