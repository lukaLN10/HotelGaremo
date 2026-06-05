namespace HotelGaremo.Application.Features.Reviews.GetUserReviews;

public class GetUserReviewsResponse
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
