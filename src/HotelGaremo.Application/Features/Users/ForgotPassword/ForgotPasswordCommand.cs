using MediatR;

namespace HotelGaremo.Application.Features.Users.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<ForgotPasswordResponse>;
