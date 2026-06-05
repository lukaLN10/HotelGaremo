using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelGaremo.Application.Features.Reviews.GetUserReviews;

public class GetUserReviewsHandler : IRequestHandler<GetUserReviewsQuery, List<GetUserReviewsResponse>>
{
    private readonly IDataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserReviewsHandler(IDataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<GetUserReviewsResponse>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(
            _httpContextAccessor.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        return await _db.Reviews
            .Where(x => x.UserId == userId)
            .Select(x => new GetUserReviewsResponse
            {
                Id = x.Id,
                Comment = x.Comment,
                Rating = x.Rating,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
