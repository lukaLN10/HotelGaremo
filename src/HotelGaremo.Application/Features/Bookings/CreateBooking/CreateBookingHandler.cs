using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using HotelGaremo.Domain.Entities;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.CreateBooking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateBookingCommand> _validator;
    private readonly IEmailSender _emailSender;

    public CreateBookingHandler(IDataContext db, IValidator<CreateBookingCommand> validator, IEmailSender emailSender)
    {
        _db = db;
        _validator = validator;
        _emailSender = emailSender;
    }

    public async Task<CreateBookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        BookingTimeWindow.EnsureWithinBookingHours();

        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId && x.IsActive, cancellationToken);

        if (user == null)
            throw new BadRequestException("მომხმარებელი ვერ მოიძებნა.");

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

        var checkIn = request.CheckIn.Date.AddHours(14);
        var checkOut = request.CheckOut.Date.AddHours(12);

        var nights = (request.CheckOut.Date - request.CheckIn.Date).Days;
        var totalPrice = nights * cottage.PricePerNight;
        var bookingNumber = $"BK-{DateTime.UtcNow:yyyyMMddHHmmss}-{request.CottageId}";

        var booking = new Booking(bookingNumber, checkIn, checkOut,
            request.GuestCount, request.UserId, request.CottageId, totalPrice, request.PaymentType);

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync(cancellationToken);

        var subject = $"ახალი ჯავშანი: {bookingNumber}";
        var body = EmailTemplateBuilder.Layout(
            subject,
            EmailTemplateBuilder.Heading("შემოვიდა ახალი ჯავშანი") +
            EmailTemplateBuilder.InfoTable(new (string, string)[]
            {
                ("ჯავშნის ნომერი", bookingNumber),
                ("კოტეჯი", cottage.CottageName),
                ("მომხმარებელი", $"{user.Name} {user.LastName} ({user.Email}, {user.PhoneNumber})"),
                ("შემოსვლა", checkIn.ToString("yyyy-MM-dd")),
                ("გასვლა", checkOut.ToString("yyyy-MM-dd")),
                ("ღამეები", nights.ToString()),
                ("სტუმრები", request.GuestCount.ToString()),
                ("ჯამური თანხა", $"{totalPrice} ₾"),
                ("გადახდის ტიპი", request.PaymentType.ToString()),
            }) +
            EmailTemplateBuilder.Paragraph("გადაამოწმეთ საბანკო ანგარიში და, თანხის ჩარიცხვის დადასტურების შემთხვევაში, დაადასტურეთ ჯავშანი ადმინ პანელიდან."));

        await _emailSender.SendEmailToAdminAsync(subject, body);

        return new CreateBookingResponse("ჯავშანი წარმატებით შეიქმნა.", bookingNumber);
    }
}
