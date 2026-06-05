using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.DeleteReview;

public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, DeleteReviewResponse>
{
    private readonly IDataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteReviewHandler(IDataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<DeleteReviewResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            throw new ValidationException(new List<ValidationFailure>
            {
                new("UserId", "ავტორიზაცია ვერ მოხერხდა.")
            });

        var review = await _db.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId && x.UserId == userId, cancellationToken);

        if (review is null)
            throw new BadRequestException("რევიუ ვერ მოიძებნა.");

        _db.Reviews.Remove(review);
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteReviewResponse("რევიუ წარმატებით წაიშალა.");
    }
}
    
