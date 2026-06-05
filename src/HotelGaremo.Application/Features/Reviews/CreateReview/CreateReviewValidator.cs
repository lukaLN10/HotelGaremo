using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Reviews.CreateReview;

public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewValidator()
    {
        RuleFor(x => x.Comment)
            .MinimumLength(5).WithMessage("კომენტარი მინიმუმ 5 სიმბოლო უნდა იყოს.")
            .MaximumLength(100).WithMessage("კომენტარი მაქსიმუმ 100 სიმბოლო უნდა იყოს.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("რეიტინგი 1-დან 5-მდე უნდა იყოს.")
            .NotNull().WithMessage("რეიტინგი ცარიელი ვერიქნება");



    }
}