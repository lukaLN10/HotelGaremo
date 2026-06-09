using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Entities;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.CreateGroupBooking;

public class CreateGroupBookingHandler : IRequestHandler<CreateGroupBookingCommand, CreateGroupBookingResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateGroupBookingCommand> _validator;

    public CreateGroupBookingHandler(IDataContext db, IValidator<CreateGroupBookingCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<CreateGroupBookingResponse> Handle(CreateGroupBookingCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userExists = await _db.Users
            .AnyAsync(x => x.Id == request.UserId && x.IsActive, cancellationToken);

        if (!userExists)
            throw new BadRequestException("მომხმარებელი ვერ მოიძებნა.");

        var checkIn = request.CheckIn.Date.AddHours(14);
        var checkOut = request.CheckOut.Date.AddHours(12);
        var nights = (request.CheckOut.Date - request.CheckIn.Date).Days;
        var groupBookingNumber = $"GRP-{DateTime.UtcNow:yyyyMMddHHmmss}";
        var bookingNumbers = new List<string>();

        var cottages = await _db.Cottages
            .Where(x => request.CottageIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var notFound = request.CottageIds.Except(cottages.Select(x => x.Id)).ToList();
        if (notFound.Any())
            throw new BadRequestException($"კოტეჯი ID:{string.Join(", ", notFound)} ვერ მოიძებნა.");

        var totalMaxGuests = cottages.Sum(x => x.MaxGuests);
        if (request.GuestCount > totalMaxGuests)
            throw new BadRequestException($"სტუმრების რაოდენობა არ უნდა აღემატებოდეს {totalMaxGuests}-ს (კოტეჯების მაქსიმუმების ჯამი).");

        foreach (var cottageId in request.CottageIds)
        {
            var cottage = cottages.First(x => x.Id == cottageId);

            var isOccupied = await _db.Bookings
                .AnyAsync(x => x.CottageId == cottageId &&
                               x.BookingStatus != BookingStatus.CanceledByAdmin &&
                               x.BookingStatus != BookingStatus.CanceledByUser &&
                               x.CheckIn < checkOut &&
                               x.CheckOut > checkIn, cancellationToken);

            if (isOccupied)
                throw new BadRequestException($"კოტეჯი ID:{cottageId} არჩეულ თარიღებზე დაკავებულია.");

            var totalPrice = nights * cottage.PricePerNight;
            var bookingNumber = $"BK-{DateTime.UtcNow:yyyyMMddHHmmss}-{cottageId}";

            var booking = new Booking(bookingNumber, checkIn, checkOut,
                request.GuestCount, request.UserId, cottageId, totalPrice, request.PaymentType);

            booking.SetGroupBookingNumber(groupBookingNumber);

            _db.Bookings.Add(booking);
            bookingNumbers.Add(bookingNumber);
        }

        await _db.SaveChangesAsync(cancellationToken);

        return new CreateGroupBookingResponse(
            "ჯგუფური ჯავშანი წარმატებით შეიქმნა.",
            groupBookingNumber,
            bookingNumbers);
    }
}
