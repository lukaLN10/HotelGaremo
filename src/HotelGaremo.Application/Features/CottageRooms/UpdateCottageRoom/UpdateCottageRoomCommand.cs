using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.CottageRooms.UpdateCottageRoom;

public record UpdateCottageRoomCommand(
    string Name,
    RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount) : IRequest<UpdateCottageRoomResponse>
{
    public int Id { get; init; }
}
