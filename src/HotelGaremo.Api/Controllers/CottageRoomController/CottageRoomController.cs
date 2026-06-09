using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.CottageRooms.CreateCottageRoom;
using HotelGaremo.Application.Features.CottageRooms.DeleteCottageRoom;
using HotelGaremo.Application.Features.CottageRooms.GetCottageRoomByCottageId;
using HotelGaremo.Application.Features.CottageRooms.GetCottageRoomById;
using HotelGaremo.Application.Features.CottageRooms.UpdateCottageRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelGaremo.Api.Controllers.CottageRoomController;

public record CreateCottageRoomRequest(
    string Name,
    Domain.Enums.RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount);

public record UpdateCottageRoomRequest(
    string Name,
    Domain.Enums.RoomType RoomType,
    bool HasJacuzzi,
    int BedCount,
    int? SofaBedCount);

[Route("api/[controller]")]
[ApiController]
public class CottageRoomController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CottageRoomController> _logger;

    public CottageRoomController(IMediator mediator, ILogger<CottageRoomController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("Create-Room/{cottageId}")]
    public async Task<IActionResult> CreateRoom(int cottageId, [FromBody] CreateCottageRoomRequest request)
    {
        var command = new CreateCottageRoomCommand(request.Name, request.RoomType, request.HasJacuzzi, request.BedCount, request.SofaBedCount, cottageId);
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<CreateCottageRoomResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpPut("Update-Room/{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateCottageRoomRequest request)
    {
        var command = new UpdateCottageRoomCommand(request.Name, request.RoomType, request.HasJacuzzi, request.BedCount, request.SofaBedCount) { Id = id };
        var response = await _mediator.Send(command);
        return Ok(new ApiResponse<UpdateCottageRoomResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpDelete("Delete-Room/{roomId}")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        var response = await _mediator.Send(new DeleteCottageRoomCommand(roomId));
        return Ok(new ApiResponse<DeleteCottageRoomResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [HttpGet("Get-Rooms-By-Cottage/{cottageId}")]
    public async Task<IActionResult> GetRoomsByCottageId(int cottageId)
    {
        var response = await _mediator.Send(new GetCottageRoomByCottageIdQuery(cottageId));
        return Ok(new ApiResponse<List<GetCottageRoomByCottageIdResponse>>
        {
            StatusCode = 200,
            Message = "ოთახები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Room/{roomId}")]
    public async Task<IActionResult> GetRoomById(int roomId)
    {
        var response = await _mediator.Send(new GetCottageRoomByIdQuery(roomId));
        return Ok(new ApiResponse<GetCottageRoomByIdResponse>
        {
            StatusCode = 200,
            Message = "ოთახი წარმატებით მოიძებნა.",
            Data = response
        });
    }
}
