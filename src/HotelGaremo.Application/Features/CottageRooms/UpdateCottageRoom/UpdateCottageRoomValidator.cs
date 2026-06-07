using FluentValidation;

namespace HotelGaremo.Application.Features.CottageRooms.UpdateCottageRoom;

public class UpdateCottageRoomValidator : AbstractValidator<UpdateCottageRoomCommand>
{
    public UpdateCottageRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ოთახის სახელი სავალდებულოა.")
            .MaximumLength(100).WithMessage("სახელი მაქსიმუმ 100 სიმბოლო.");

        RuleFor(x => x.BedCount)
            .GreaterThan(0).WithMessage("საწოლების რაოდენობა უნდა იყოს 0-ზე მეტი.")
            .When(x => x.RoomType != Domain.Enums.RoomType.BATHROOM &&
                       x.RoomType != Domain.Enums.RoomType.BATHROOM_JACUZZI &&
                       x.RoomType != Domain.Enums.RoomType.WET_POINT);

        RuleFor(x => x.BedCount)
            .Equal(0).WithMessage("ამ ტიპის ოთახს საწოლი არ უნდა ჰქონდეს.")
            .When(x => x.RoomType == Domain.Enums.RoomType.BATHROOM ||
                       x.RoomType == Domain.Enums.RoomType.BATHROOM_JACUZZI ||
                       x.RoomType == Domain.Enums.RoomType.WET_POINT);
    }
}
