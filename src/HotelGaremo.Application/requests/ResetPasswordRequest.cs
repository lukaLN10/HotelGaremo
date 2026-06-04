namespace HotelGaremo.Application.requests;

public record ResetPasswordRequest(int Code, string NewPassword);
