using HotelGaremo.Application.Abstraction;
using HotelGaremo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotelGaremo.Infrastructure.Services;

public class BookingExpirationService : BackgroundService
{
    private static readonly TimeSpan ExpirationWindow = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan CheckInterval = TimeSpan.FromMinutes(1);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BookingExpirationService> _logger;

    public BookingExpirationService(IServiceScopeFactory scopeFactory, ILogger<BookingExpirationService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(CheckInterval);

        do
        {
            try
            {
                await ExpirePendingBookingsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ვადაგასული ჯავშნების შემოწმება ვერ შესრულდა.");
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task ExpirePendingBookingsAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IDataContext>();

        var cutoff = DateTime.UtcNow - ExpirationWindow;

        var expiredBookings = await db.Bookings
            .Where(x => x.BookingStatus == BookingStatus.Pending && x.CreatedAt < cutoff)
            .ToListAsync(cancellationToken);

        if (!expiredBookings.Any())
            return;

        foreach (var booking in expiredBookings)
        {
            booking.ChangeStatus(BookingStatus.Expired);
        }

        await db.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("გაუქმდა {Count} ვადაგასული ჯავშანი.", expiredBookings.Count);
    }
}
