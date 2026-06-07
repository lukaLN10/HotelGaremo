using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomById;

public class GetCottageRoomByIdHandler : IRequestHandler<GetCottageRoomByIdQuery, GetCottageRoomByIdResponse>
{
    private readonly IDataContext _db;

    public GetCottageRoomByIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<GetCottageRoomByIdResponse> Handle(GetCottageRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _db.CottageRooms
            .FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);

        if (room == null)
            throw new BadRequestException("ოთახი ვერ მოიძებნა.");

        return new GetCottageRoomByIdResponse(
            room.Id,
            room.Name,
            room.RoomType,
            room.HasJacuzzi,
            room.BedCount,
            room.SofaBedCount);
    }
}
