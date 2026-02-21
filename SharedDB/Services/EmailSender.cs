using Microsoft.AspNetCore.Identity;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace SharedDB.Services;

public class EmailSender 
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Confirm your email";
        message.Body = new TextPart("plain")
        {
            Text = $"Please confirm your account by clicking this link: {confirmationLink}"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
            await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Reset your password";
        message.Body = new TextPart("plain")
        {
            Text = $"Please reset your password by clicking this link: {resetLink}"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
            await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Reset your password";
        message.Body = new TextPart("plain")
        {
            Text = $"Your password reset code is: {resetCode}"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
            await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}