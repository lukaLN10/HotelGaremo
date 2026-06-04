using MediatR;

namespace HotelGaremo.Application.Features.Users.GetUsers;

public record GetUsersQuery() : IRequest<List<GetUsersResponse>>;
