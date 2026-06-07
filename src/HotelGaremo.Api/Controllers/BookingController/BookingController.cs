using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Bookings.CancelBooking;
using HotelGaremo.Application.Features.Bookings.ChangeBookingStatus;
using HotelGaremo.Application.Features.Bookings.CreateBooking;
using HotelGaremo.Application.Features.Bookings.GetAllBookings;
using HotelGaremo.Application.Features.Bookings.GetBookingById;
using HotelGaremo.Application.Features.Bookings.GetCottageBookings;
using HotelGaremo.Application.Features.Bookings.GetMyBookings;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelGaremo.Api.Controllers.BookingController;

public record CreateBookingRequest(
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    int CottageId,
    PaymentType PaymentType);

public record ChangeBookingStatusRequest(BookingStatus BookingStatus);

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IMediator mediator, ILogger<BookingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("Create-Booking/{userId}")]
    public async Task<IActionResult> CreateBooking(int userId, [FromBody] CreateBookingRequest request)
    {
        var command = new CreateBookingCommand(request.CheckIn, request.CheckOut, request.GuestCount, request.CottageId, request.PaymentType) { UserId = userId };
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<CreateBookingResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpPut("Cancel-Booking/{bookingId}/{userId}")]
    public async Task<IActionResult> CancelBooking(int bookingId, int userId)
    {
        var command = new CancelBookingCommand(bookingId) { UserId = userId };
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<CancelBookingResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpGet("Get-My-Bookings/{userId}")]
    public async Task<IActionResult> GetMyBookings(int userId)
    {
        var response = await _mediator.Send(new GetMyBookingsQuery(userId));
        return Ok(new ApiResponse<List<GetMyBookingsResponse>>
        {
            StatusCode = 200,
            Message = "ჯავშნები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-All-Bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        var response = await _mediator.Send(new GetAllBookingsQuery());
        return Ok(new ApiResponse<List<GetAllBookingsResponse>>
        {
            StatusCode = 200,
            Message = "ჯავშნები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Booking/{bookingId}")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var response = await _mediator.Send(new GetBookingByIdQuery(bookingId));
        return Ok(new ApiResponse<GetBookingByIdResponse>
        {
            StatusCode = 200,
            Message = "ჯავშანი წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpPut("Change-Status/{bookingId}")]
    public async Task<IActionResult> ChangeBookingStatus(int bookingId, [FromBody] ChangeBookingStatusRequest request)
    {
        var command = new ChangeBookingStatusCommand(bookingId, request.BookingStatus);
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<ChangeBookingStatusResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpGet("Get-Cottage-Bookings/{cottageId}")]
    public async Task<IActionResult> GetCottageBookings(int cottageId)
    {
        var response = await _mediator.Send(new GetCottageBookingsQuery(cottageId));
        return Ok(new ApiResponse<List<GetCottageBookingsResponse>>
        {
            StatusCode = 200,
            Message = "კოტეჯის ჯავშნები წარმატებით მოიძებნა.",
            Data = response
        });
    }
}
