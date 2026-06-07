using FluentValidation;

namespace HotelGaremo.Application.Features.Bookings.CreateBooking;

public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.CheckIn)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("შემოსვლის თარიღი არ შეიძლება წარსულში იყოს.");

        RuleFor(x => x.CheckOut)
            .GreaterThan(x => x.CheckIn).WithMessage("გასვლის თარიღი უნდა იყოს შემოსვლის თარიღზე გვიან.");

        RuleFor(x => x.GuestCount)
            .GreaterThan(0).WithMessage("სტუმრების რაოდენობა უნდა იყოს 0-ზე მეტი.");

        RuleFor(x => x.CottageId)
            .GreaterThan(0).WithMessage("კოტეჯის ID სავალდებულოა.");
    }
}
