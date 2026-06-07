using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Bookings.GetBookingById;

public record GetBookingByIdResponse(
    int Id,
    string BookingNumber,
    string? GroupBookingNumber,
    string CottageName,
    string UserName,
    string UserLastName,
    string UserEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    PaymentType PaymentType,
    BookingStatus BookingStatus);
