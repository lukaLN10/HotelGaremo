using FluentValidation;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.UpdateCottage;

public class UpdateCottageHandler : IRequestHandler<UpdateCottageCommand, UpdateCottageResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<UpdateCottageCommand> _validator;

    public UpdateCottageHandler(IDataContext db, IValidator<UpdateCottageCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<UpdateCottageResponse> Handle(UpdateCottageCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cottage = await _db.Cottages
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (cottage == null)
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა.");

        cottage.Update(request.CottageName, request.Description, request.RoomCount, request.PricePerNight, request.MaxGuests);

        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateCottageResponse("კოტეჯი წარმატებით განახლდა.");
    }
}
