using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArchitectureSample.Infrastructure.Email;

public class EmailSender(IOptions<EmailOptions> options, IHostEnvironment environment) : IEmailSender
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Subject = subject,
            Body = new TextPart("html")
            {
                Text = htmlMessage
            }
        };

        message.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
        message.To.Add(new MailboxAddress(email, email));

        using var client = new SmtpClient();

        if (environment.IsDevelopment())
        {
            client.ServerCertificateValidationCallback = (_, _, _, _) => true;
        }

        var host = _options.Smtp.Host;
        var port = _options.Smtp.Port;

        //var serviceUrl = _configuration.GetServiceUri("email", "smtp");
        //if (serviceUrl != null)
        //{
        //    host = serviceUrl.Host;
        //    port = serviceUrl.Port;
        //}

        await client.ConnectAsync(host, port, _options.Smtp.UseSsl);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}
