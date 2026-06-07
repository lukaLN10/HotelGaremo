using MediatR;

namespace HotelGaremo.Application.Features.CottageRooms.DeleteCottageRoom;

public record DeleteCottageRoomCommand(int RoomId) : IRequest<DeleteCottageRoomResponse>;
