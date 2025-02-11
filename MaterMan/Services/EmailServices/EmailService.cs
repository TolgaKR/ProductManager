using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MaterMan.Services.EmailServices
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(string toEmail,string subject,string body )
        {
            using(var smtpClient= new SmtpClient(_config["EmailSettings:SMTPHost"]))
            {
                smtpClient.Port = int.Parse(_config["EmailSettings:SMTPPort"]);
                smtpClient.Credentials = new NetworkCredential(
               _config["EmailSettings:SenderEmail"],
               _config["EmailSettings:SenderPassword"]
           );
                smtpClient.EnableSsl = true;
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_config["EmailSettings:SenderEmail"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }



    }
}
