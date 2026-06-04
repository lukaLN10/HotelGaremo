using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Users.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<ResetPasswordCommand> _validator;

    public ResetPasswordHandler(IDataContext db, IValidator<ResetPasswordCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
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

        if (user.PasswordRecoveryCode != request.Code)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("Code", "კოდი არასწორია.")
            });
        }

        if (user.PasswordRecoveryCodeExpiry == null || user.PasswordRecoveryCodeExpiry < DateTime.UtcNow)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("Code", "კოდის მოქმედების ვადა გასულია.")
            });
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.ChangePassword(hashedPassword);
        user.ClearPasswordRecoveryCode();

        await _db.SaveChangesAsync(cancellationToken);

        return new ResetPasswordResponse("პაროლი წარმატებით შეიცვალა.");
    }
}
