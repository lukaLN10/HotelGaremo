using MediatR;

namespace HotelGaremo.Application.Features.Bookings.GetMyBookings;

public record GetMyBookingsQuery(int UserId) : IRequest<List<GetMyBookingsResponse>>;
