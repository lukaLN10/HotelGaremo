using HotelGaremo.Domain.Enums;

namespace HotelGaremo.Application.Features.Users.GetUserById;

public record GetUserByIdResponse(
    int Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    UserRoles Role,
    bool IsVerified,
    DateTime CreatedAt,
    List<UserBookingDto> Bookings,
    List<UserReviewDto> Reviews);

public record UserBookingDto(
    string BookingNumber,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    decimal TotalPrice,
    PaymentType PaymentType,
    BookingStatus BookingStatus);

public record UserReviewDto(
    int Id,
    string Comment,
    int Rating);
