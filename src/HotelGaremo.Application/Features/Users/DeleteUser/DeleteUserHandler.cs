using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelGaremo.Application.Features.Users.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IDataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteUserHandler(IDataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            throw new ValidationException(new List<ValidationFailure>
            {
                new("UserId", "ავტორიზაცია ვერ მოხერხდა.")
            });

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
            throw new BadRequestException("მომხმარებელი ვერ მოიძებნა.");

        user.Deactivate();
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteUserResponse("მომხმარებელის დეაქტივაცია წარმატებით დასრულდა ");
    }
}
