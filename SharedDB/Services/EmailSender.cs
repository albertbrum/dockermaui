using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace SharedDB.Services
{
    public class EmailSender : IEmailSender<IdentityUser>
    {
        public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
            => SendEmailAsync(email, "Confirme seu e-mail",
                $"<html><body>Confirme sua conta <a href='{confirmationLink}'>clicando aqui</a>.</body></html>");

        public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
            => SendEmailAsync(email, "Redefinir senha",
                $"<html><body>Redefina sua senha <a href='{resetLink}'>clicando aqui</a>.</body></html>");

        public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
            => SendEmailAsync(email, "Código de redefinição",
                $"<html><body>Use o código para redefinir sua senha:<br>{resetCode}</body></html>");


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var client = new SmtpClient("localhost", 25) { EnableSsl = false, DeliveryMethod = SmtpDeliveryMethod.Network, UseDefaultCredentials = false };

            var mailMessage = new MailMessage
            (
                from: "teste@meuapp.com",
                to: email,
                subject: subject,
                body: htmlMessage
            )
            { IsBodyHtml = true };

            await client.SendMailAsync(mailMessage);
        }
    }
}
