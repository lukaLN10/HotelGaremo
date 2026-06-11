using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Bookings.GetMyBookings;

public record GetMyBookingsResponse(
    int Id,
    string BookingNumber,
    string? GroupBookingNumber,
    string CottageName,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    PaymentType PaymentType,
    BookingStatus BookingStatus,
    DateTime CreatedAt);
