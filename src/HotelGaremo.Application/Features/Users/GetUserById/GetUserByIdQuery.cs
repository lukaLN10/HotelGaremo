using MediatR;

namespace HotelGaremo.Application.Features.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<GetUserByIdResponse>;
