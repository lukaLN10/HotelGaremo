using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Features.Users.GetUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.GetAllCottages;

public class GetAllCottagesHandler : IRequestHandler<GetAllCottagesQuery, List<GetAllCottagesResponse>>
{
    private readonly IDataContext _db;

    public GetAllCottagesHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<List<GetAllCottagesResponse>> Handle(GetAllCottagesQuery request, CancellationToken cancellationToken)
    {
        return await _db.Cottages
          .Select(x => new GetAllCottagesResponse(
              x.Id,
              x.CottageName,
              x.Description,
              x.RoomCount,
              x.PricePerNight,
              x.MaxGuests))
          .ToListAsync(cancellationToken);

    }
}
