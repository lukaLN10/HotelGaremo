using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.SendPasswordRecoveryCode;

public class SendPasswordRecoveryCodeHandler : IRequestHandler<SendPasswordRecoveryCodeCommand, SendPasswordRecoveryCodeResponse>
{
    private readonly IDataContext _db;
    private readonly IEmailSender _emailSender;

    public SendPasswordRecoveryCodeHandler(
        IDataContext db,
        IEmailSender emailSender)
    {
        _db = db;
        _emailSender = emailSender;
    }

    public async Task<SendPasswordRecoveryCodeResponse> Handle(
        SendPasswordRecoveryCodeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(
            x => x.Email == request.email,
            cancellationToken);

        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure(
                    "Email",
                    "მეილი არასწორია, ან იუზერი არ არსებობს.")
            });
        }

        var recoveryCode = Random.Shared.Next(100000, 999999);

        user.SetPasswordRecoveryCode(recoveryCode);

        await _db.SaveChangesAsync(cancellationToken);

        await _emailSender.SendEmailToUserAsync(
            user.Email,
            "Password Recovery",
            $"Your password recovery code is: {recoveryCode}");

        return new SendPasswordRecoveryCodeResponse(
            "პაროლის აღსადგენი კოდი წარმატებით გამოიგზავნა."
        );
    }
}
