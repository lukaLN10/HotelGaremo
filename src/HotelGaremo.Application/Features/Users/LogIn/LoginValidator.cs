using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.LogIn;


public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("ემაილი სავალდებულოა.")
            .EmailAddress().WithMessage("ემაილი არასწორი ფორმატისაა.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("პაროლი სავალდებულოა.");
    }
}