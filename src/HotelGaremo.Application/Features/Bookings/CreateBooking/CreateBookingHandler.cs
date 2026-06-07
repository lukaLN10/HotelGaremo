using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Entities;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.CreateBooking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateBookingCommand> _validator;

    public CreateBookingHandler(IDataContext db, IValidator<CreateBookingCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<CreateBookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cottage = await _db.Cottages
            .FirstOrDefaultAsync(x => x.Id == request.CottageId, cancellationToken);

        if (cottage == null)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        if (request.GuestCount > cottage.MaxGuests)
            throw new BadRequestException($"სტუმრების რაოდენობა არ უნდა აღემატებოდეს {cottage.MaxGuests}-ს.");

        var isOccupied = await _db.Bookings
            .AnyAsync(x => x.CottageId == request.CottageId &&
                           x.BookingStatus != BookingStatus.CanceledByAdmin &&
                           x.BookingStatus != BookingStatus.CanceledByUser &&
                           x.CheckIn < request.CheckOut &&
                           x.CheckOut > request.CheckIn, cancellationToken);

        if (isOccupied)
            throw new BadRequestException("კოტეჯი არჩეულ თარიღებზე დაკავებულია.");

        var nights = (request.CheckOut - request.CheckIn).Days;
        var totalPrice = nights * cottage.PricePerNight;
        var bookingNumber = $"BK-{DateTime.UtcNow:yyyyMMddHHmmss}-{request.CottageId}";

        var booking = new Booking(bookingNumber, request.CheckIn, request.CheckOut,
            request.GuestCount, request.UserId, request.CottageId, totalPrice, request.PaymentType);

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateBookingResponse("ჯავშანი წარმატებით შეიქმნა.", bookingNumber);
    }
}
