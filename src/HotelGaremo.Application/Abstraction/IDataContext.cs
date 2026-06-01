using HotelGaremo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Abstraction;

public interface IDataContext
{
    DbSet<Booking> Bookings { get; }
    DbSet<Cottage> Cottages { get; }
    DbSet<CottageImage> CottageImages { get; }
    DbSet<CottageRoom> CottageRooms { get; }
    DbSet<Review> Reviews { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default);

}
