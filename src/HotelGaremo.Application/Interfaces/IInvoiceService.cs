namespace HotelGaremo.Application.Interfaces;

public record InvoiceData(
    int BookingId,
    string GuestName,
    string GuestEmail,
    string CottageName,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    decimal DepositAmount
);

public interface IInvoiceService
{
    byte[] GenerateBookingInvoice(InvoiceData data);
}