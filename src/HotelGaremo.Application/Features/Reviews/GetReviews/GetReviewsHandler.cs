using HotelGaremo.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.GetReviews;

public class GetReviewsHandler : IRequestHandler<GetReviewsQuery, List<GetReviewsResponse>>
{
    private readonly IDataContext _db;

    public GetReviewsHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetReviewsResponse>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Reviews
            .Include(x => x.User)
            .Select(x => new GetReviewsResponse
            {
               Comment = x.Comment,
               Rating = x.Rating,
               UserName = x.User!.Name + " " + x.User!.LastName
            })
              .ToListAsync(cancellationToken);
    }
}
