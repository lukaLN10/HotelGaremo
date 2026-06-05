using FluentValidation;

namespace HotelGaremo.Application.Features.Reviews.UpdateReview;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(x => x.Comment)
            .MinimumLength(5).WithMessage("კომენტარი მინიმუმ 5 სიმბოლო უნდა იყოს.")
            .MaximumLength(100).WithMessage("კომენტარი მაქსიმუმ 100 სიმბოლო უნდა იყოს.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("რეიტინგი 1-დან 5-მდე უნდა იყოს.")
            .NotNull().WithMessage("რეიტინგი ცარიელი ვერიქნება");
    }
}
