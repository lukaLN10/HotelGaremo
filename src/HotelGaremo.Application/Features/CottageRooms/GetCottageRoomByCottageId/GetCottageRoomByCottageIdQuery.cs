using MediatR;

namespace HotelGaremo.Application.Features.CottageRooms.GetCottageRoomByCottageId;

public record GetCottageRoomByCottageIdQuery(int CottageId) : IRequest<List<GetCottageRoomByCottageIdResponse>>;
