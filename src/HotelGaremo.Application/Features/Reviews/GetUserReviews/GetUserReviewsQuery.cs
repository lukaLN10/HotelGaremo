using MediatR;

namespace HotelGaremo.Application.Features.Reviews.GetUserReviews;

public record GetUserReviewsQuery : IRequest<List<GetUserReviewsResponse>>;
