using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Cottages.GetCottageBookedDates;

public record GetCottageBookedDatesResponse(
    DateTime CheckIn,
    DateTime CheckOut,
    BookingStatus BookingStatus);
