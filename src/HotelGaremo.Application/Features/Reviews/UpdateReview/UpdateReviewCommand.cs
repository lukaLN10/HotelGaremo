using MediatR;

namespace HotelGaremo.Application.Features.Reviews.UpdateReview;

public record UpdateReviewCommand(
    int ReviewId,
    string Comment,
    int Rating
) : IRequest<UpdateReviewResponse>;
