using HotelGaremo.Application.Interfaces;
using HotelGaremo.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Infrastructure.Services;

internal class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailToUserAsync(string to, string subject, string content)
    {
        await SendAsync(to, subject, content);
    }

    public async Task SendEmailToAdminAsync(string subject, string content)
    {
        await SendAsync(_emailSettings.AdminEmail, subject, content);
    }

    private async Task SendAsync(string to, string subject, string content)
    {
        using var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            EnableSsl = _emailSettings.EnableSsl,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                _emailSettings.Email,
                _emailSettings.Password
            )
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(_emailSettings.Email),
            Subject = subject,
            Body = content,
            IsBodyHtml = true
        };

        mail.To.Add(to);

        await smtpClient.SendMailAsync(mail);
    }
}
