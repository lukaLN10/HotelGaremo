using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Cottages.GetAvailableCottages;

public class GetAvailableCottagesHandler : IRequestHandler<GetAvailableCottagesQuery, List<GetAvailableCottagesResponse>>
{
    private readonly IDataContext _db;

    public GetAvailableCottagesHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetAvailableCottagesResponse>> Handle(GetAvailableCottagesQuery request, CancellationToken cancellationToken)
    {
        var checkIn = request.CheckIn.Date.AddHours(14);
        var checkOut = request.CheckOut.Date.AddHours(12);
        var nights = (request.CheckOut.Date - request.CheckIn.Date).Days;

        if (nights <= 0)
            throw new BadRequestException("გასვლის თარიღი უნდა იყოს შემოსვლის თარიღზე გვიან.");

        var occupiedCottageIds = await _db.Bookings
            .Where(x => x.BookingStatus != BookingStatus.CanceledByAdmin &&
                        x.BookingStatus != BookingStatus.CanceledByUser &&
                        x.CheckIn < checkOut &&
                        x.CheckOut > checkIn)
            .Select(x => x.CottageId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var availableCottages = await _db.Cottages
            .Where(x => !occupiedCottageIds.Contains(x.Id))
            .Select(x => new GetAvailableCottagesResponse(
                x.Id,
                x.CottageName,
                x.Description,
                x.RoomCount,
                x.PricePerNight,
                x.MaxGuests,
                x.PricePerNight * nights))
            .ToListAsync(cancellationToken);

        return availableCottages;
    }
}
