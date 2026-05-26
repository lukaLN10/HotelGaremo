using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class CottageImage
{
    public string ImageURL { get; set; }
    public int? CottageId { get; set; }
    public Cottage? Cottage { get; set; }
    public int? CottageRoomId { get; set; }
    public CottageRoom? CottageRoom { get; set; }
}
