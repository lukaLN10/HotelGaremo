using MediatR;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomById;

public record GetCottageRoomByIdQuery(int RoomId) : IRequest<GetCottageRoomByIdResponse>;
