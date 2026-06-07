using FluentValidation;
using FluentValidation.Results;
using HotelGaremo.Application.Abstraction;
using HotelGaremo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.CreateCottage;

public class CreateCottageHandler : IRequestHandler<CreateCottageCommand, CreateCottageResponse>
{
    private readonly IDataContext _db;
    private readonly IValidator<CreateCottageCommand> _validator;

    public CreateCottageHandler(IDataContext db, IValidator<CreateCottageCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<CreateCottageResponse> Handle(CreateCottageCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var errors = new List<ValidationFailure>();

        var existingCottage = await _db.Cottages
            .FirstOrDefaultAsync(x => x.CottageName == request.CottageName, cancellationToken);

        if (existingCottage != null)
            errors.Add(new ValidationFailure("CottageName", "კოტეჯი ამ სახელით უკვე არსებობს."));

        if (errors.Any())
            throw new ValidationException(errors);

        var cottage = new Cottage(request.CottageName, request.Description, request.RoomCount, request.PricePerNight, request.MaxGuests);

        _db.Cottages.Add(cottage);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateCottageResponse("კოტეჯი წარმატებით დაემატა.");
    }
}
