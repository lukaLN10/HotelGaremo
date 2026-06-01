using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Interfaces;
using HotelGaremo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateUserCommand> validator;
    private readonly IEmailSender emailSender;

    public CreateUserHandler(IDataContext db, IValidator<CreateUserCommand> validator, IEmailSender emailSender)
    {
        _db = db;
        this.validator = validator;
        this.emailSender = emailSender;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(
    request,
    cancellationToken);

        var verificationCode = Random.Shared.Next(100000, 999999);

        var user = new User(
            request.Name,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            request.Password,
            request.PhoneNumber,
            verificationCode);

        await _db.Users.AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await emailSender.SendEmailToUserAsync(
            request.Email,
            "Verification Code",
            $"Your verification code is: {verificationCode}");

        return new CreateUserResponse(user.Id);

    }
}
