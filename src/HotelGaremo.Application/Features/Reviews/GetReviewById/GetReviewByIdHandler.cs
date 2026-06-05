using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.Reviews.GetReviewById;

public class GetReviewByIdHandler : IRequestHandler<GetReviewByIdQuery, GetReviewByIdResponse>
{
    private readonly IDataContext _db;

    public GetReviewByIdHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<GetReviewByIdResponse> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _db.Reviews
            .Include(x => x.User)
            .Where(x => x.Id == request.Id)
            .Select(x => new GetReviewByIdResponse
            {
                Id = x.Id,
                Comment = x.Comment,
                Rating = x.Rating,
                UserName = x.User!.Name + " " + x.User!.LastName
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (review is null)
            throw new BadRequestException("რევიუ ვერ მოიძებნა.");

        return review;
    }
}
