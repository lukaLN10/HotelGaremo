using MediatR;

namespace HotelGaremo.Application.Features.CottageImage.GetCottageRoomImages;

public record GetCottageRoomImagesQuery(int CottageRoomId) : IRequest<List<GetCottageRoomImagesResponse>>;
