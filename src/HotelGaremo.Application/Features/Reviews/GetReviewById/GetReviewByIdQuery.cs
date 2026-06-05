using MediatR;

namespace HotelGaremo.Application.Features.Reviews.GetReviewById;

public record GetReviewByIdQuery(int Id) : IRequest<GetReviewByIdResponse>;
