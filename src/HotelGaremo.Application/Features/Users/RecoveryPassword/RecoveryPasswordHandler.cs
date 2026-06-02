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

namespace HotelGaremo.Application.Features.Users.RecoveryPassword;

public class RecoveryPasswordHandler : IRequestHandler<RecoveryPasswordCommand, RecoveryPasswordResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<RecoveryPasswordCommand> _validator;

    public RecoveryPasswordHandler(IDataContext db, IValidator<RecoveryPasswordCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<RecoveryPasswordResponse> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(
         x => x.Email == request.email,
         cancellationToken);

        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
        {
            new ValidationFailure("Email", "მეილი არასწორია.")
        });
        }

        if (user.PasswordRecoveryCode == null)
        {
            throw new ValidationException(new List<ValidationFailure>
        {
            new ValidationFailure("Code", "აღდგენის კოდი არ არის გენერირებული.")
        });
        }

        if (user.PasswordRecoveryCode != request.code)
        {
            throw new ValidationException(new List<ValidationFailure>
        {
            new ValidationFailure("Code", "აღდგენის კოდი არასწორია.")
        });
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.newPassord);


        user.ChangePassword(hashedPassword);
        user.ClearPasswordRecoveryCode();

        await _db.SaveChangesAsync(cancellationToken);

        return new RecoveryPasswordResponse(
            "პაროლი წარმატებით შეიცვალა.");



    }
}
