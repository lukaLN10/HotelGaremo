using HotelGaremo.Domain.Enums;
using MediatR;

namespace HotelGaremo.Application.Features.CottageRooms.CreateCottageRoom;

public record CreateCottageRoomCommand(
    string Name,
    RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount,
    int CottageId) : IRequest<CreateCottageRoomResponse>;
