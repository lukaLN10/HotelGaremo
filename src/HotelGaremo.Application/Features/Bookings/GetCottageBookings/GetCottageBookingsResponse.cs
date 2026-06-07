using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Bookings.GetCottageBookings;

public record GetCottageBookingsResponse(
    int Id,
    string BookingNumber,
    string UserName,
    string UserLastName,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    PaymentType PaymentType,
    BookingStatus BookingStatus);
