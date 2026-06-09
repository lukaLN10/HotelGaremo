using HotelGaremo.Application.Common;
using HotelGaremo.Application.Features.Reviews.CreateReview;
using HotelGaremo.Application.Features.Reviews.DeleteReview;
using HotelGaremo.Application.Features.Reviews.DeleteReviewByAdmin;
using HotelGaremo.Application.Features.Reviews.GetReviewById;
using HotelGaremo.Application.Features.Reviews.GetReviews;
using HotelGaremo.Application.Features.Reviews.GetUserReviews;
using HotelGaremo.Application.Features.Reviews.UpdateReview;
using HotelGaremo.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelGaremo.Api.Controllers.ReviewControler;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ILogger<ReviewController> logger;

    public ReviewController(IMediator mediator, ILogger<ReviewController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }
    [Authorize]
    [HttpPost("Create-Review")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        var response = await mediator.Send(command);
        logger.LogInformation("Review created by user: {UserId}", response.UserId);
        return Ok(new ApiResponse<CreateReviewResponse>
        {
            StatusCode = 200,
            Message = "Review წარმატებით დაემატა ",
            Data = response

        });

    }
    [HttpGet("Get-Reviews")]
    public async Task<IActionResult> GetReviews()
    {
        var response = await mediator.Send(new GetReviewsQuery());
        return Ok(new ApiResponse<List<GetReviewsResponse>>
        {
            StatusCode = 200,
            Message = "რევიუები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [Authorize]
    [HttpGet("Get-User-Reviews")]
    public async Task<IActionResult> GetUserReviews()
    {
        var response = await mediator.Send(new GetUserReviewsQuery());
        return Ok(new ApiResponse<List<GetUserReviewsResponse>>
        {
            StatusCode = 200,
            Message = "თქვენი რევიუები წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [HttpGet("Get-Review-By-Id/{reviewId}")]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        var response = await mediator.Send(new GetReviewByIdQuery(reviewId));
        return Ok(new ApiResponse<GetReviewByIdResponse>
        {
            StatusCode = 200,
            Message = "რევიუ წარმატებით მოიძებნა.",
            Data = response
        });
    }

    [Authorize]
    [HttpPut("Update-Review/{reviewId}")]
    public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] UpdateReviewCommand command)
    {
        var response = await mediator.Send(command with { ReviewId = reviewId });
        logger.LogInformation("Review updated: {ReviewId}", response.ReviewId);
        return Ok(new ApiResponse<UpdateReviewResponse>
        {
            StatusCode = 200,
            Message = response.Message,
            Data = response
        });
    }

    [Authorize]
    [HttpDelete("Delete-Review/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        var response = await mediator.Send(new DeleteReviewCommand(reviewId));
        logger.LogInformation("Review deleted: {ReviewId}", reviewId);
        return Ok(new ApiResponse<DeleteReviewResponse>
        {
            StatusCode = 200,
            Message = response.message,
            Data = response
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete-Review-By-Admin/{reviewId}")]
    public async Task<IActionResult> DeleteReviewByAdmin(int reviewId)
    {
        var response = await mediator.Send(new DeleteReviewByAdminCommand(reviewId));
        logger.LogInformation("Review deleted by admin. ReviewId: {ReviewId}", reviewId);
        return Ok(new ApiResponse<DeleteReviewByAdminResponse>
        {
            StatusCode = 200,
            Message = response.message,
            Data = response
        });
    }
}
