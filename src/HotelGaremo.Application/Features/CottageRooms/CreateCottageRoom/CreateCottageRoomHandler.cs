using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using HotelGaremo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelGaremo.Application.Features.CottageRooms.CreateCottageRoom;

public class CreateCottageRoomHandler : IRequestHandler<CreateCottageRoomCommand, CreateCottageRoomResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateCottageRoomCommand> _validator;

    public CreateCottageRoomHandler(IDataContext db, IValidator<CreateCottageRoomCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<CreateCottageRoomResponse> Handle(CreateCottageRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cottageExists = await _db.Cottages
            .AnyAsync(x => x.Id == request.CottageId, cancellationToken);

        if (!cottageExists)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        var room = new CottageRoom(request.RoomType, request.Name, request.HasJacuzzi, request.BedCount, request.SofaBedCount, request.CottageId);

        _db.CottageRooms.Add(room);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateCottageRoomResponse("ოთახი წარმატებით დაემატა.");
    }
}
