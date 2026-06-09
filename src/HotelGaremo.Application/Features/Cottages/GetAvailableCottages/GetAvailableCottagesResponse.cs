namespace HotelGaremo.Application.Features.Cottages.GetAvailableCottages;

public record GetAvailableCottagesResponse(
    int Id,
    string CottageName,
    string Description,
    int RoomCount,
    decimal PricePerNight,
    int MaxGuests,
    decimal TotalPrice);
