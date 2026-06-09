using MediatR;

namespace HotelGaremo.Application.Features.Cottages.GetAvailableCottages;

public record GetAvailableCottagesQuery(DateTime CheckIn, DateTime CheckOut) : IRequest<List<GetAvailableCottagesResponse>>;
