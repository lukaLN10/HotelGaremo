using MediatR;

namespace HotelGaremo.Application.Features.Users.ResetPassword;

public record ResetPasswordCommand(string Email, int Code, string NewPassword) : IRequest<ResetPasswordResponse>;
