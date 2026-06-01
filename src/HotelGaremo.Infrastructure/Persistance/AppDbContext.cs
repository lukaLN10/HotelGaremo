using HotelGaremo.Application.Abstraction;
using HotelGaremo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Infrastructure.Persistance;

public class AppDbContext : DbContext, IDataContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Cottage> Cottages => Set<Cottage>();
    public DbSet<CottageImage> CottageImages => Set<CottageImage>();
    public DbSet<CottageRoom> CottageRooms => Set<CottageRoom>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
