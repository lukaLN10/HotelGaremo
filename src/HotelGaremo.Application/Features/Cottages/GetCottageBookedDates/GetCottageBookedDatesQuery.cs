using MediatR;

namespace HotelGaremo.Application.Features.Cottages.GetCottageBookedDates;

public record GetCottageBookedDatesQuery(int CottageId) : IRequest<List<GetCottageBookedDatesResponse>>;
