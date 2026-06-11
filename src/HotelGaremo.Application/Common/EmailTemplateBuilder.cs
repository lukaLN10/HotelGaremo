using System.Text;

namespace HotelGaremo.Application.Common;

public static class EmailTemplateBuilder
{
    private const string Green = "#2D4A35";
    private const string Gold = "#C9A84C";
    private const string Cream = "#F5EFE6";
    private const string TextDark = "#1a1a1a";
    private const string TextMuted = "#6b7280";

    public static string Layout(string title, string contentHtml, string? footerNote = null)
    {
        return $@"
<!DOCTYPE html>
<html lang=""ka"">
<head>
<meta charset=""UTF-8"" />
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
<title>{title}</title>
</head>
<body style=""margin:0;padding:0;background-color:{Cream};font-family:'Segoe UI',Arial,sans-serif;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:{Cream};padding:24px 0;"">
    <tr>
      <td align=""center"">
        <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px;width:100%;background-color:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 4px 16px rgba(0,0,0,0.06);"">
          <tr>
            <td style=""background-color:{Green};padding:28px 32px;text-align:center;"">
              <span style=""font-family:Georgia,'Playfair Display',serif;font-size:28px;font-weight:700;letter-spacing:2px;color:#ffffff;"">GAREMO</span>
              <div style=""height:2px;width:60px;background-color:{Gold};margin:12px auto 0;""></div>
            </td>
          </tr>
          <tr>
            <td style=""padding:32px;color:{TextDark};font-size:15px;line-height:1.7;"">
              {contentHtml}
            </td>
          </tr>
          <tr>
            <td style=""background-color:{Green};padding:20px 32px;text-align:center;"">
              <p style=""margin:0;color:#ffffff;font-size:13px;opacity:0.85;"">© {DateTime.UtcNow.Year} Garemo Hotel & Cottages</p>
              {(footerNote is null ? "" : $@"<p style=""margin:6px 0 0;color:{Gold};font-size:12px;"">{footerNote}</p>")}
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
    }

    public static string Heading(string text)
    {
        return $@"<h2 style=""margin:0 0 16px;font-family:Georgia,'Playfair Display',serif;font-size:22px;color:{Green};"">{text}</h2>";
    }

    public static string Paragraph(string text)
    {
        return $@"<p style=""margin:0 0 16px;color:{TextMuted};"">{text}</p>";
    }

    public static string InfoTable(IEnumerable<(string Label, string Value)> rows)
    {
        var sb = new StringBuilder();
        sb.Append($@"<table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:collapse;margin:0 0 16px;"">");

        foreach (var (label, value) in rows)
        {
            sb.Append($@"
              <tr>
                <td style=""padding:8px 12px;background-color:{Cream};border-bottom:1px solid #ffffff;font-weight:600;color:{Green};font-size:13px;width:40%;"">{label}</td>
                <td style=""padding:8px 12px;background-color:{Cream};border-bottom:1px solid #ffffff;color:{TextDark};font-size:13px;"">{value}</td>
              </tr>");
        }

        sb.Append("</table>");
        return sb.ToString();
    }

    public static string CodeBox(string code)
    {
        return $@"
            <div style=""text-align:center;margin:24px 0;"">
              <span style=""display:inline-block;padding:16px 32px;background-color:{Cream};border:2px dashed {Gold};border-radius:8px;font-family:Georgia,'Playfair Display',serif;font-size:32px;font-weight:700;letter-spacing:8px;color:{Green};"">{code}</span>
            </div>";
    }
}
