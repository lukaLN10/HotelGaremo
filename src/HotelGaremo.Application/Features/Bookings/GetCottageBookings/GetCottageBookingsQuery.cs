using MediatR;

namespace HotelGaremo.Application.Features.Bookings.GetCottageBookings;

public record GetCottageBookingsQuery(int CottageId) : IRequest<List<GetCottageBookingsResponse>>;
