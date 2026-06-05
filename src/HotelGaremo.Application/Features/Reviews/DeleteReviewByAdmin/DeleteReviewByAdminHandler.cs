using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.DeleteReviewByAdmin;

public class DeleteReviewByAdminHandler : IRequestHandler<DeleteReviewByAdminCommand, DeleteReviewByAdminResponse>
{
    private readonly IDataContext _db;

    public DeleteReviewByAdminHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<DeleteReviewByAdminResponse> Handle(DeleteReviewByAdminCommand request, CancellationToken cancellationToken)
    {
        var review = await _db.Reviews.FirstOrDefaultAsync(x => x.Id == request.reviewId, cancellationToken);

        if (review is null)
            throw new BadRequestException("რევიუ ვერ მოიძებნა.");

        _db.Reviews.Remove(review);
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteReviewByAdminResponse("რევიუ წარმატებით წაიშალა.");
    }
}
