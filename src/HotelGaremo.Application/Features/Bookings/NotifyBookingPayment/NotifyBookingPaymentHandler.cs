using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Bookings.NotifyBookingPayment;

public class NotifyBookingPaymentHandler : IRequestHandler<NotifyBookingPaymentCommand, NotifyBookingPaymentResponse>
{
    private readonly IDataContext _db;
    private readonly IEmailSender _emailSender;

    public NotifyBookingPaymentHandler(IDataContext db, IEmailSender emailSender)
    {
        _db = db;
        _emailSender = emailSender;
    }

    public async Task<NotifyBookingPaymentResponse> Handle(NotifyBookingPaymentCommand request, CancellationToken cancellationToken)
    {
        var booking = await _db.Bookings
            .Include(x => x.Cottage)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.BookingNumber == request.BookingNumber, cancellationToken);

        if (booking == null)
            throw new BadRequestException("ჯავშანი ვერ მოიძებნა.");

        if (booking.UserId != request.UserId)
            throw new BadRequestException("თქვენ არ გაქვთ ამ ჯავშნის შესახებ შეტყობინების გაგზავნის უფლება.");

        var subject = $"გადახდის შეტყობინება: {booking.BookingNumber}";
        var body = EmailTemplateBuilder.Layout(
            subject,
            EmailTemplateBuilder.Heading("მომხმარებელმა განაცხადა, რომ თანხა ჩარიცხა") +
            EmailTemplateBuilder.InfoTable(new (string, string)[]
            {
                ("ჯავშნის ნომერი", booking.BookingNumber),
                ("კოტეჯი", booking.Cottage.CottageName),
                ("მომხმარებელი", $"{booking.User.Name} {booking.User.LastName} ({booking.User.Email}, {booking.User.PhoneNumber})"),
                ("თანხა", $"{booking.TotalPrice} ₾"),
                ("გადახდის ტიპი", booking.PaymentType.ToString()),
            }) +
            EmailTemplateBuilder.Paragraph("გთხოვთ გადაამოწმოთ საბანკო ანგარიში და, თანხის ჩარიცხვის დადასტურების შემთხვევაში, დაადასტუროთ ჯავშანი ადმინ პანელიდან."));

        await _emailSender.SendEmailToAdminAsync(subject, body);

        return new NotifyBookingPaymentResponse("შეტყობინება წარმატებით გაიგზავნა.");
    }
}
