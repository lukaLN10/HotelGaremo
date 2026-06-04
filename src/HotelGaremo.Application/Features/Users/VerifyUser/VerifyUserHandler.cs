using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.VerifyUser;

public class VerifyUserHandler : IRequestHandler<VerifyUserCommand, VerifyUserResponse>
{
    private readonly IDataContext _db;

    public VerifyUserHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<VerifyUserResponse> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("Email", "მეილი არასწორია.")
            });
        }

        if (user.IsVerified)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("Email", "ანგარიში უკვე დადასტურებულია.")
            });
        }

        if (user.VerificationCode != request.VerificationCode)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("VerificationCode", "ვერიფიკაციის კოდი არასწორია.")
            });
        }

        user.Verify();
        await _db.SaveChangesAsync(cancellationToken);

        return new VerifyUserResponse("ვერიფიკაცია წარმატებით დასრულდა.");
    }
}