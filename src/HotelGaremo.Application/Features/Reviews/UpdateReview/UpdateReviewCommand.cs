using MediatR;
using System.Text.Json.Serialization;

namespace HotelGaremo.Application.Features.Reviews.UpdateReview;

public record UpdateReviewCommand(
    string Comment,
    int Rating
) : IRequest<UpdateReviewResponse>
{
    [JsonIgnore]
    public int ReviewId { get; init; }
}
