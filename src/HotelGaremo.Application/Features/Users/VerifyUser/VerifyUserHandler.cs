using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.email);
        
        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
                {
                  new ValidationFailure("Email", "მეილი არასწორია.")
                });
        }

        if(user.VerificationCode != request.verificationCode)
        {
            throw new ValidationException(new List<ValidationFailure>
                {
                  new ValidationFailure("VerificationCode", "ვერიფიკაციის კოდი არასწორია.")
                });
        }

        user.Verify();
        await _db.SaveChangesAsync(cancellationToken);
        return new VerifyUserResponse(user.Id);





    }
}
