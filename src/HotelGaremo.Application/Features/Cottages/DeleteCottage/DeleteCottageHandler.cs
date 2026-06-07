using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.DeleteCottage;

internal class DeleteCottageHandler : IRequestHandler<DeleteCottageCommand, DeleteCottageResponse>
{
    private readonly IDataContext _db;

    public DeleteCottageHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<DeleteCottageResponse> Handle(DeleteCottageCommand request, CancellationToken cancellationToken)
    {
        var cottage = await _db.Cottages.FirstOrDefaultAsync(x => x.Id == request.CottageId);

        if (cottage == null)
        {
            throw new BadRequestException("კოტეჯი ვერ მოიძებნა");
        }

        _db.Cottages.Remove(cottage);
        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteCottageResponse("კოტეჯი წარმატებით წაიშალა");


    }
}
