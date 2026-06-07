using MediatR;

namespace HotelGaremo.Application.Features.Cottages.GetCottageById;

public record GetCottageByIdQuery(int CottageId) : IRequest<GetCottageByIdResponse>;
