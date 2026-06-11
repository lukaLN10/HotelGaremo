namespace HotelGaremo.Application.Common;

public static class BookingTimeWindow
{
    private static readonly TimeSpan GeorgiaOffset = TimeSpan.FromHours(4);
    private static readonly TimeSpan StartHour = TimeSpan.FromHours(12);
    private static readonly TimeSpan EndHour = TimeSpan.FromHours(20);

    public static void EnsureWithinBookingHours()
    {
        var georgiaTime = DateTime.UtcNow.Add(GeorgiaOffset).TimeOfDay;

        if (georgiaTime < StartHour || georgiaTime >= EndHour)
            throw new BadRequestException("ჯავშანი შეგიძლიათ 12:00-დან 20:00 საათის ინტერვალში.");
    }
}
