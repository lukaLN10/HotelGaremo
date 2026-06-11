using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Features.Cottages.GetAllCottages;

public record GetAllCottagesResponse(
    int Id,
    string CottageName,
    string Description,
    int RoomCount,
    decimal PricePerNight,
    int MaxGuests,
    List<string> ImageUrls);