namespace HotelGaremo.Application.Features.Bookings.CreateGroupBooking;

public record CreateGroupBookingResponse(
    string Message,
    string GroupBookingNumber,
    List<string> BookingNumbers);
