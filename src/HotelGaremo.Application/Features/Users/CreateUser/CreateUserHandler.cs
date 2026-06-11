using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using HotelGaremo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

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
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var verificationCode = Random.Shared.Next(100000, 999999);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var errors = new List<ValidationFailure>();

        var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser is not null)
        {
            if (!existingUser.IsActive)
            {
                existingUser.Reactivate(hashedPassword, verificationCode);
                await _db.SaveChangesAsync(cancellationToken);

                await emailSender.SendEmailToUserAsync(
                    request.Email,
                    "თქვენი ვერიფიკაციის კოდი",
                    EmailTemplateBuilder.Layout(
                        "თქვენი ვერიფიკაციის კოდი",
                        EmailTemplateBuilder.Heading("მოგესალმებით Garemo-ში!") +
                        EmailTemplateBuilder.Paragraph("რეგისტრაციის დასასრულებლად შეიყვანეთ ქვემოთ მოცემული ვერიფიკაციის კოდი:") +
                        EmailTemplateBuilder.CodeBox(verificationCode.ToString()) +
                        EmailTemplateBuilder.Paragraph("თუ ეს მოთხოვნა თქვენ არ გაგზავნიათ, უგულებელყავით ეს წერილი.")));

                return new CreateUserResponse(existingUser.Id);
            }

            errors.Add(new ValidationFailure("Email", "ეს მეილი უკვე რეგისტრირებულია."));
        }

        if (await _db.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken))
            errors.Add(new ValidationFailure("PhoneNumber", "ეს ტელეფონის ნომერი უკვე რეგისტრირებულია."));

        if (errors.Any())
            throw new ValidationException(errors);

        var user = new User(
            request.Name,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            hashedPassword,
            request.PhoneNumber,
            verificationCode);

        await _db.Users.AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await emailSender.SendEmailToUserAsync(
            request.Email,
            "თქვენი ვერიფიკაციის კოდი",
            EmailTemplateBuilder.Layout(
                "თქვენი ვერიფიკაციის კოდი",
                EmailTemplateBuilder.Heading("მოგესალმებით Garemo-ში!") +
                EmailTemplateBuilder.Paragraph("რეგისტრაციის დასასრულებლად შეიყვანეთ ქვემოთ მოცემული ვერიფიკაციის კოდი:") +
                EmailTemplateBuilder.CodeBox(verificationCode.ToString()) +
                EmailTemplateBuilder.Paragraph("თუ ეს მოთხოვნა თქვენ არ გაგზავნიათ, უგულებელყავით ეს წერილი.")));

        return new CreateUserResponse(user.Id);
    }
}

