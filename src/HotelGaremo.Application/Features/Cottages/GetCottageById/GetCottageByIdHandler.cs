using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Cottages.GetCottageById;

public class GetCottageByIdHandler : IRequestHandler<GetCottageByIdQuery, GetCottageByIdResponse>
{
    private readonly IDataContext _db;

    public GetCottageByIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<GetCottageByIdResponse> Handle(GetCottageByIdQuery request, CancellationToken cancellationToken)
    {
        var cottage = await _db.Cottages
            .Include(x => x.Bookings)
            .Include(x => x.CottageRooms)
            .FirstOrDefaultAsync(x => x.Id == request.CottageId, cancellationToken);

        if (cottage == null)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        return new GetCottageByIdResponse(
            cottage.Id,
            cottage.CottageName,
            cottage.Description,
            cottage.RoomCount,
            cottage.PricePerNight,
            cottage.MaxGuests,
            cottage.Bookings.Select(b => new CottageBookingDto(
                b.BookingNumber,
                b.CheckIn,
                b.CheckOut,
                b.GuestCount,
                b.TotalPrice,
                b.BookingStatus)).ToList(),
            cottage.CottageRooms.Select(r => new CottageRoomDto(
                r.Id,
                r.Name,
                r.RoomType,
                r.HasJacuzzi,
                r.BedCount,
                r.SofaBedCount)).ToList());
    }
}
