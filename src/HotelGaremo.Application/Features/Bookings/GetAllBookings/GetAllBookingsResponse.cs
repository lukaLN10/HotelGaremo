using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Bookings.GetAllBookings;

public record GetAllBookingsResponse(
    int Id,
    string BookingNumber,
    string? GroupBookingNumber,
    string CottageName,
    string UserName,
    string UserLastName,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    PaymentType PaymentType,
    BookingStatus BookingStatus);
