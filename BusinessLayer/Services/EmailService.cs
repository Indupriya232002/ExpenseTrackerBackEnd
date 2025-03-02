using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmailService
    {
        public static void Send(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")  // SMTP server for Gmail
            {
                Port = 587,
                Credentials = new NetworkCredential("eindupriya@gmail.com", "svuy ccum jvnx pffv"),  // Your email and password
                EnableSsl = true,
            };

            smtpClient.Send("eindupriya@gmail.com", toEmail, subject, body);  // Sender email and recipient's email
        }

    }
}
