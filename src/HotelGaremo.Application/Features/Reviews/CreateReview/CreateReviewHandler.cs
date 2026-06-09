using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Entities;
using HotelGaremo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.CreateReview;

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, CreateReviewResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateReviewCommand> _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateReviewHandler(IDataContext db, IValidator<CreateReviewCommand> validator, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _validator = validator;
        this._httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateReviewResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userId = int.Parse(
            _httpContextAccessor.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var hasBooking = await _db.Bookings
            .AnyAsync(b => b.UserId == userId && b.BookingStatus == BookingStatus.Completed, cancellationToken);

        if (!hasBooking)
            throw new BadRequestException("რევიუს დასაწერად მინიმუმ 1 დასრულებული ჯავშანი გჭირდება.");

        var review = new Review(request.Comment, request.Rating, userId);

        await _db.Reviews.AddAsync(review, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateReviewResponse
        {
            Message = "რევიუ წარმატებით დაემატა.",
            UserId = userId
        };





    }
}
