using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.UpdateCottage;

public record UpdateCottageCommand(
    string CottageName,
    string Description,
    int RoomCount,
    decimal PricePerNight,
    int MaxGuests) : IRequest<UpdateCottageResponse>
{
    public int Id { get; init; }
}