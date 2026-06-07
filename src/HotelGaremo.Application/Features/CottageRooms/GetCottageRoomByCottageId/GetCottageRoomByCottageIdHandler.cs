using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomByCottageId;

public class GetCottageRoomByCottageIdHandler : IRequestHandler<GetCottageRoomByCottageIdQuery, List<GetCottageRoomByCottageIdResponse>>
{
    private readonly IDataContext _db;

    public GetCottageRoomByCottageIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetCottageRoomByCottageIdResponse>> Handle(GetCottageRoomByCottageIdQuery request, CancellationToken cancellationToken)
    {
        var cottageExists = await _db.Cottages
            .AnyAsync(x => x.Id == request.CottageId, cancellationToken);

        if (!cottageExists)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        return await _db.CottageRooms
            .Where(x => x.CottageId == request.CottageId)
            .Select(x => new GetCottageRoomByCottageIdResponse(
                x.Id,
                x.Name,
                x.RoomType,
                x.HasJacuzzi,
                x.BedCount,
                x.SofaBedCount))
            .ToListAsync(cancellationToken);
    }
}
