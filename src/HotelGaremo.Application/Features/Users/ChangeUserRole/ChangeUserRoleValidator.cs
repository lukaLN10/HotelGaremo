using FluentValidation;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public class ChangeUserRoleValidator : AbstractValidator<ChangeUserRoleCommand>
{
    public ChangeUserRoleValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Role).IsInEnum();
    }
}
