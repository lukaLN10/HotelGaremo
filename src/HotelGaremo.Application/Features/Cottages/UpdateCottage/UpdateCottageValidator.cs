using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.UpdateCottage;

public class UpdateCottageValidator : AbstractValidator<UpdateCottageCommand>
{
    public UpdateCottageValidator()
    {
        RuleFor(x => x.CottageName)
            .NotEmpty().WithMessage("კოტეჯის სახელი სავალდებულოა.")
            .MaximumLength(100).WithMessage("სახელი მაქსიმუმ 100 სიმბოლო.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("აღწერა სავალდებულოა.")
            .MaximumLength(1000).WithMessage("აღწერა მაქსიმუმ 1000 სიმბოლო.");

        RuleFor(x => x.RoomCount)
            .GreaterThan(0).WithMessage("ოთახების რაოდენობა უნდა იყოს 0-ზე მეტი.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("ფასი უნდა იყოს 0-ზე მეტი.");

        RuleFor(x => x.MaxGuests)
            .GreaterThan(0).WithMessage("სტუმრების რაოდენობა უნდა იყოს 0-ზე მეტი.");
    }
}
