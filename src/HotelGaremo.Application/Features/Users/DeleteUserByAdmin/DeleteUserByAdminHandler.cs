using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Users.DeleteUserByAdmin;

public class DeleteUserByAdminHandler : IRequestHandler<DeleteUserByAdminCommand, DeleteUserByAdminResponse>
{
    private readonly IDataContext _db;

    public DeleteUserByAdminHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<DeleteUserByAdminResponse> Handle(DeleteUserByAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.userId, cancellationToken);

        if (user is null)
            throw new BadRequestException("მომხმარებელი ვერ მოიძებნა.");

        user.Deactivate();
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteUserByAdminResponse("მომხმარებელის დეაქტივაცია წარმატებით დასრულდა");
    }
}