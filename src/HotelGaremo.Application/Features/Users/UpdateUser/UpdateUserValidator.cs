using FluentValidation;

namespace HotelGaremo.Application.Features.Users.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow).WithMessage("დაბადების თარიღი უნდა იყოს წარსულში.");
    }
}
