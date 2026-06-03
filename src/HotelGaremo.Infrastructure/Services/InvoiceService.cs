using HotelGaremo.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HotelGaremo.Infrastructure.Services;

public class InvoiceService : IInvoiceService
{
    public byte[] GenerateBookingInvoice(InvoiceData data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("HotelGaremo — ჯავშნის ინვოისი")
                    .SemiBold().FontSize(20).AlignCenter();

                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Item().Text($"ჯავშნის ნომერი: #{data.BookingId}");
                    col.Item().Text($"სტუმარი: {data.GuestName}");
                    col.Item().Text($"ემაილი: {data.GuestEmail}");
                    col.Item().Text($"კოტეჯი: {data.CottageName}");
                    col.Item().Text($"შესვლა: {data.CheckIn:dd/MM/yyyy}");
                    col.Item().Text($"გასვლა: {data.CheckOut:dd/MM/yyyy}");
                    col.Item().PaddingTop(10)
                        .Text($"სულ: {data.TotalPrice} ₾").SemiBold();
                    col.Item()
                        .Text($"დეპოზიტი (50%): {data.DepositAmount} ₾")
                        .FontColor(Colors.Red.Medium);
                });

                page.Footer()
                    .AlignCenter()
                    .Text($"გენერირდა: {DateTime.Now:dd/MM/yyyy HH:mm}");
            });
        });

        return document.GeneratePdf();
    }
}