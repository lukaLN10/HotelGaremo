using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageRooms.DeleteCottageRoom;

public class DeleteCottageRoomHandler : IRequestHandler<DeleteCottageRoomCommand, DeleteCottageRoomResponse>
{
    private readonly IDataContext _db;

    public DeleteCottageRoomHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<DeleteCottageRoomResponse> Handle(DeleteCottageRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _db.CottageRooms
            .FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);

        if (room == null)
            throw new BadRequestException("ოთახი ვერ მოიძებნა.");

        _db.CottageRooms.Remove(room);
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteCottageRoomResponse("ოთახი წარმატებით წაიშალა.");
    }
}
