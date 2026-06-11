using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Enums;

public enum BookingStatus
{
    Pending,
    Approved,
    CanceledByAdmin,
    CanceledByUser,
    CheckedIn,
    CheckedOut,
    NoShow,
    Completed,
    Expired,
}
