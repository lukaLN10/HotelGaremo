using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomById;

public record GetCottageRoomByIdResponse(
    int Id,
    string Name,
    RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount,
    int CottageId,
    string CottageName);
