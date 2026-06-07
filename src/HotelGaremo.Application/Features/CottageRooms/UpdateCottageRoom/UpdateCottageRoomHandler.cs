using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageRooms.UpdateCottageRoom;

public class UpdateCottageRoomHandler : IRequestHandler<UpdateCottageRoomCommand, UpdateCottageRoomResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<UpdateCottageRoomCommand> _validator;

    public UpdateCottageRoomHandler(IDataContext db, IValidator<UpdateCottageRoomCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<UpdateCottageRoomResponse> Handle(UpdateCottageRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var room = await _db.CottageRooms
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (room == null)
            throw new BadRequestException("ოთახი ვერ მოიძებნა.");

        room.Update(request.RoomType, request.Name, request.HasJacuzzi, request.BedCount, request.SofaBedCount);

        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateCottageRoomResponse("ოთახი წარმატებით განახლდა.");
    }
}
