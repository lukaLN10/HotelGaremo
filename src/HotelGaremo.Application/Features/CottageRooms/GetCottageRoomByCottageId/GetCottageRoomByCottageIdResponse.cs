using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomByCottageId;

public record GetCottageRoomByCottageIdResponse(
    int Id,
    string Name,
    RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount);
