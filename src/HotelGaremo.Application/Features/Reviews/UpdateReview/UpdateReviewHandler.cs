using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelGaremo.Application.Features.Reviews.UpdateReview;

public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, UpdateReviewResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<UpdateReviewCommand> _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateReviewHandler(IDataContext db, IValidator<UpdateReviewCommand> validator, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateReviewResponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userId = int.Parse(
            _httpContextAccessor.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var review = await _db.Reviews
            .FirstOrDefaultAsync(x => x.Id == request.ReviewId && x.UserId == userId, cancellationToken);

        if (review is null)
            throw new BadRequestException("რევიუ ვერ მოიძებნა.");

        review.Update(request.Comment, request.Rating);
        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateReviewResponse
        {
            Message = "რევიუ წარმატებით განახლდა.",
            ReviewId = review.Id
        };
    }
}
