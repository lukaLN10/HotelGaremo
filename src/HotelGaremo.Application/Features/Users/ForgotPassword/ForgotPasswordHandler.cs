
using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.ForgotPassword;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<ForgotPasswordCommand> _validator;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordHandler(IDataContext db, IValidator<ForgotPasswordCommand> validator, IEmailSender emailSender)
    {
        _db = db;
        _validator = validator;
        _emailSender = emailSender;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("Email", "მეილი არასწორია.")
            });
        }

        var code = Random.Shared.Next(100000, 999999);
        user.SetPasswordRecoveryCode(code);

        await _db.SaveChangesAsync(cancellationToken);

        await _emailSender.SendEmailToUserAsync(
            request.Email,
            "პაროლის აღდგენის კოდი",
            EmailTemplateBuilder.Layout(
                "პაროლის აღდგენის კოდი",
                EmailTemplateBuilder.Heading("პაროლის აღდგენა") +
                EmailTemplateBuilder.Paragraph("პაროლის აღსადგენად შეიყვანეთ ქვემოთ მოცემული კოდი:") +
                EmailTemplateBuilder.CodeBox(code.ToString()) +
                EmailTemplateBuilder.Paragraph("თუ ეს მოთხოვნა თქვენ არ გაგზავნიათ, უგულებელყავით ეს წერილი.")));

        return new ForgotPasswordResponse("პაროლის აღდგენის კოდი გამოგზავნილია თქვენს მეილზე.");
    }
}
