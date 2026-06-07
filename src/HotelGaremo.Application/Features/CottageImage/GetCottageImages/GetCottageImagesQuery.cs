using MediatR;

namespace HotelGaremo.Application.Features.CottageImage.GetCottageImages;

public record GetCottageImagesQuery(int CottageId) : IRequest<List<GetCottageImagesResponse>>;
