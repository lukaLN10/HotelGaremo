using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelGaremo.Application.Features.Users.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<UpdateUserCommand> _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateUserHandler(IDataContext db, IValidator<UpdateUserCommand> validator, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            throw new ValidationException(new List<ValidationFailure>
            {
                new("UserId", "ავტორიზაცია ვერ მოხერხდა.")
            });

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
            throw new ValidationException(new List<ValidationFailure>
            {
                new("UserId", "მომხმარებელი ვერ მოიძებნა.")
            });

        user.UpdateProfile(request.Name, request.LastName, request.PhoneNumber, request.DateOfBirth);
        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse("პროფილი წარმატებით განახლდა.");
    }
}
