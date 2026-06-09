using FluentValidation;

namespace HotelGaremo.Application.Features.Bookings.CreateGroupBooking;

public class CreateGroupBookingValidator : AbstractValidator<CreateGroupBookingCommand>
{
    public CreateGroupBookingValidator()
    {
        RuleFor(x => x.CheckIn)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("შემოსვლის თარიღი არ შეიძლება წარსულში იყოს.");

        RuleFor(x => x.CheckOut)
            .GreaterThan(x => x.CheckIn).WithMessage("გასვლის თარიღი უნდა იყოს შემოსვლის თარიღზე გვიან.");

        RuleFor(x => x.GuestCount)
            .GreaterThan(0).WithMessage("სტუმრების რაოდენობა უნდა იყოს 0-ზე მეტი.");

        RuleFor(x => x.CottageIds)
            .NotEmpty().WithMessage("მინიმუმ 1 კოტეჯი უნდა იყოს მითითებული.")
            .Must(x => x.Count >= 2).WithMessage("ჯგუფური ჯავშნისთვის მინიმუმ 2 კოტეჯი უნდა მიუთითოთ.")
            .Must(x => x.Distinct().Count() == x.Count).WithMessage("კოტეჯები არ უნდა მეორდებოდეს.");

        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage("მითითებული გადახდის ტიპი არ არსებობს.");
    }
}
