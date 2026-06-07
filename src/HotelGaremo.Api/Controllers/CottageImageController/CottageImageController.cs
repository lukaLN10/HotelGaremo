using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.CottageImage.DeleteCottageImage;
using HotelGaremo.Application.Features.CottageImage.GetCottageImages;
using HotelGaremo.Application.Features.CottageImage.GetCottageRoomImages;
using HotelGaremo.Application.Features.CottageImage.UploadCottageImage;
using HotelGaremo.Application.Features.CottageImage.UploadCottageRoomImage;
using HotelGaremo.Application.requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CottageImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public CottageImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

 //[Authorize]
[HttpPost("Upload-Cottage-Image/{cottageId}")]
public async Task<IActionResult> UploadCottageImage(int cottageId, [FromForm] AddImage request)
{
    var command = new UploadCottageImageCommand(request.File, cottageId);
    var response = await _mediator.Send(command);
    return Ok(new ApiResponse<UploadCottageImageResponse>
    {
        StatusCode = 200,
        Message = "კოტეჯის სურათი წარმატებით აიტვირთა.",
        Data = response
    });
}

[Authorize]
[HttpPost("Upload-Room-Image/{cottageRoomId}")]
public async Task<IActionResult> UploadCottageRoomImage(int cottageRoomId, [FromForm] AddImage request)
{
    var command = new UploadCottageRoomImageCommand(request.File, cottageRoomId);
    var response = await _mediator.Send(command);
    return Ok(new ApiResponse<UploadCottageRoomImageResponse>
    {
        StatusCode = 200,
        Message = "ოთახის სურათი წარმატებით აიტვირთა.",
        Data = response
    });
}

    [HttpGet("Get-Cottage-Images/{cottageId}")]
    public async Task<IActionResult> GetCottageImages(int cottageId)
    {
        var response = await _mediator.Send(new GetCottageImagesQuery(cottageId));
        return Ok(new ApiResponse<List<GetCottageImagesResponse>>
        {
            StatusCode = 200,
            Message = "კოტეჯის სურათები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Room-Images/{cottageRoomId}")]
    public async Task<IActionResult> GetCottageRoomImages(int cottageRoomId)
    {
        var response = await _mediator.Send(new GetCottageRoomImagesQuery(cottageRoomId));
        return Ok(new ApiResponse<List<GetCottageRoomImagesResponse>>
        {
            StatusCode = 200,
            Message = "ოთახის სურათები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    //[Authorize]
    [HttpDelete("Delete-Image/{imageId}")]
    public async Task<IActionResult> DeleteImage(int imageId)
    {
        var response = await _mediator.Send(new DeleteCottageImageCommand(imageId));
        return Ok(new ApiResponse<DeleteCottageImageResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }
}
