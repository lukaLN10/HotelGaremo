using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleCommand, ChangeUserRoleResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<ChangeUserRoleCommand> _validator;

    public ChangeUserRoleHandler(IDataContext db, IValidator<ChangeUserRoleCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<ChangeUserRoleResponse> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user is null)
            throw new ValidationException(new List<ValidationFailure>
            {
                new("UserId", "მომხმარებელი ვერ მოიძებნა.")
            });

        user.ChangeRole(request.Role);
        await _db.SaveChangesAsync(cancellationToken);

        return new ChangeUserRoleResponse("როლი წარმატებით შეიცვალა.");
    }
}
