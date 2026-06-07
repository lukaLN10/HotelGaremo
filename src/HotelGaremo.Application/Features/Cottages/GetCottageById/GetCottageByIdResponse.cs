using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Cottages.GetCottageById;

public record GetCottageByIdResponse(
    int Id,
    string CottageName,
    string Description,
    int RoomCount,
    decimal PricePerNight,
    int MaxGuests,
    List<CottageBookingDto> Bookings,
    List<CottageRoomDto> CottageRooms);

public record CottageBookingDto(
    string BookingNumber,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    BookingStatus BookingStatus);

public record CottageRoomDto(
    int Id,
    string Name,
    RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount);
