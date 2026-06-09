using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Cottages.CreateCottage;
using HotelGaremo.Application.Features.Cottages.DeleteCottage;
using HotelGaremo.Application.Features.Cottages.GetAllCottages;
using HotelGaremo.Application.Features.Cottages.GetAvailableCottages;
using HotelGaremo.Application.Features.Cottages.GetCottageById;
using HotelGaremo.Application.Features.Cottages.UpdateCottage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelGaremo.Api.Controllers.CottageControler;

public record UpdateCottageRequest(
    string CottageName,
    string Description,
    int RoomCount,
    decimal PricePerNight,
    int MaxGuests);

[Route("api/[controller]")]
[ApiController]
public class CottageController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CottageController> _logger;

    public CottageController(IMediator mediator, ILogger<CottageController> logger)
    {
        this._mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Create-Cottage")]
    public async Task<IActionResult> CreateCottage([FromBody] CreateCottageCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<CreateCottageResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("Update-Cottage/{id}")]
    public async Task<IActionResult> UpdateCottage(int id, [FromBody] UpdateCottageRequest request)
    {
        var command = new UpdateCottageCommand(request.CottageName, request.Description, request.RoomCount, request.PricePerNight, request.MaxGuests) { Id = id };
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<UpdateCottageResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpGet (" Get-All-Cottages")]
    public async Task<IActionResult> GetAllCottages()
    {
        var response = await _mediator.Send(new GetAllCottagesQuery());
        return Ok(new ApiResponse<List<GetAllCottagesResponse>>
        {
            StatusCode = 200,
            Message = "კოტეჯები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Cottage/{id}")]
    public async Task<IActionResult> GetCottageById(int id)
    {
        var response = await _mediator.Send(new GetCottageByIdQuery(id));
        return Ok(new ApiResponse<GetCottageByIdResponse>
        {
            StatusCode = 200,
            Message = "კოტეჯი წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Available-Cottages")]
    public async Task<IActionResult> GetAvailableCottages([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
    {
        var response = await _mediator.Send(new GetAvailableCottagesQuery(checkIn, checkOut));
        return Ok(new ApiResponse<List<GetAvailableCottagesResponse>>
        {
            StatusCode = 200,
            Message = "თავისუფალი კოტეჯები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete-Cottage/{cottageId}")]
    public async Task<IActionResult> DeleteCottage(int cottageId)
    {
        var response = await _mediator.Send(new DeleteCottageCommand(cottageId));
        return Ok(new ApiResponse<DeleteCottageResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

}
