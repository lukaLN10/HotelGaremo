using FluentValidation;

namespace HotelGaremo.Application.Features.Bookings.ChangeBookingStatus;

public class ChangeBookingStatusValidator : AbstractValidator<ChangeBookingStatusCommand>
{
    public ChangeBookingStatusValidator()
    {
        RuleFor(x => x.BookingStatus)
            .IsInEnum().WithMessage("მითითებული სტატუსი არ არსებობს.");
    }
}
