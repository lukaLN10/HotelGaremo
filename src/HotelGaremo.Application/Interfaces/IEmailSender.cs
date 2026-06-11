using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Application.Interfaces;

public interface IEmailSender
{
    Task SendEmailToUserAsync(string to, string subject, string content);
    Task SendEmailToAdminAsync(string subject, string content);
}
