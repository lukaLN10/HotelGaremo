using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Cottages.GetCottageBookedDates;

public class GetCottageBookedDatesHandler : IRequestHandler<GetCottageBookedDatesQuery, List<GetCottageBookedDatesResponse>>
{
    private readonly IDataContext _db;

    public GetCottageBookedDatesHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetCottageBookedDatesResponse>> Handle(GetCottageBookedDatesQuery request, CancellationToken cancellationToken)
    {
        var cottageExists = await _db.Cottages
            .AnyAsync(x => x.Id == request.CottageId, cancellationToken);

        if (!cottageExists)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        return await _db.Bookings
            .Where(x => x.CottageId == request.CottageId &&
                        x.BookingStatus != BookingStatus.CanceledByAdmin &&
                        x.BookingStatus != BookingStatus.CanceledByUser)
            .Select(x => new GetCottageBookedDatesResponse(
                x.CheckIn,
                x.CheckOut,
                x.BookingStatus))
            .ToListAsync(cancellationToken);
    }
}
