using HotelGaremo.Domain.Common;
using HotelGaremo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class CottageRoom : BaseEntity
{
    public RoomType RoomType { get; private set; }
    public string Name { get; private set; }
    public bool HasJacuzzi { get; private set; }
    public int BedCount { get; private set; }
    public int? SofaBedCount { get; private set; }


    public int CottageId { get; private set; }
    public Cottage Cottage { get; private set; }

    public List<CottageImage> RoomImages { get; private set; } = new List<CottageImage>();

    private CottageRoom() { }

    public CottageRoom(RoomType roomType, string name, bool hasJacuzzi, int bedCount, int? sofaBedCount, int cottageId)
    {
        RoomType = roomType;
        Name = name;
        HasJacuzzi = hasJacuzzi;
        BedCount = bedCount;
        SofaBedCount = sofaBedCount;
        CottageId = cottageId;
    }

    public void Update(RoomType roomType, string name, bool hasJacuzzi, int bedCount, int? sofaBedCount)
    {
        RoomType = roomType;
        Name = name;
        HasJacuzzi = hasJacuzzi;
        BedCount = bedCount;
        SofaBedCount = sofaBedCount;
    }
}
