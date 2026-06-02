using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Features.Users.VerifyUser;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.ChangeUserRole;

public class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleCommand, ChangeUserRoleResponse>
{
    private readonly IDataContext _db;

    public ChangeUserRoleHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<ChangeUserRoleResponse> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.email);

        if (user == null)
        {
            throw new ValidationException(new List<ValidationFailure>
                {
                  new ValidationFailure("Email", "მეილი არასწორია.")
                });
        }



        user.ChangeRole(request.role);
        await _db.SaveChangesAsync(cancellationToken);
        return new ChangeUserRoleResponse(user.Id, user.Role);





    }
}
