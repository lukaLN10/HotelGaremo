using HotelGaremo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class Cottage : BaseEntity
{
    public string CottageName { get; private set; }
    public string Description { get; private set; }
    public int RoomCount { get; private set; }
    public decimal PricePerNight { get; private set; }
    public int MaxGuests { get; private set; }


    public List<CottageRoom> CottageRooms { get; private set; } = new List<CottageRoom>();
    public List<Booking> Bookings { get; private set; } = new List<Booking>();
    public List<CottageImage> CottageImages { get; private set; } = new List<CottageImage>();


    private Cottage() { }

    public Cottage(string cottageName, string description, int roomCount, decimal pricePerNight, int maxGuests)
    {
        CottageName = cottageName;
        Description = description;
        RoomCount = roomCount;
        PricePerNight = pricePerNight;
        MaxGuests = maxGuests;
    }


}
