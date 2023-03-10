using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly IOptions<EmailConfiguration> emailConfiguration;

    public EmailService(IOptions<EmailConfiguration> emailConfiguration)
    {
        this.emailConfiguration = emailConfiguration;
    }

    public async Task SendConfirmationEmailAsync(string emailAddress,
        string token,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var subject = "Confirm your email";
        var urlEncodedToken = WebUtility.UrlEncode(token);
        var message = $"Please confirm your email by clicking this link: {this.emailConfiguration.Value.FrontendUrl}/confirm-email?token={urlEncodedToken}&userId={userId}";

        await this.SendEmailAsync(emailAddress, subject, message, cancellationToken);
    }

    public async Task SendEmailAsync(string emailAddress,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        if (this.emailConfiguration == null)
        {
            Console.WriteLine("Email configuration is null");
            return;
        }

        var mail = new MailMessage();
        var smtpServer = new SmtpClient(this.emailConfiguration.Value.Host, this.emailConfiguration.Value.Port);


        mail.From = new MailAddress(this.emailConfiguration.Value.Username);
        mail.To.Add(emailAddress);
        mail.Subject = subject;
        mail.Body = message;

        smtpServer.Credentials = this.emailConfiguration.Value.Authentication ? new NetworkCredential(this.emailConfiguration.Value.Username, this.emailConfiguration.Value.Password) : null;
        smtpServer.EnableSsl = this.emailConfiguration.Value.UseSSL;

        await smtpServer.SendMailAsync(mail, cancellationToken);
    }
}

public interface IEmailService
{
    Task SendConfirmationEmailAsync(string emailAddress,
        string token,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task SendEmailAsync(string emailAddress,
        string subject,
        string message,
        CancellationToken cancellationToken = default);
}

public class EmailConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool UseSSL { get; set; }
    public string FrontendUrl { get; set; }
    public bool Authentication { get; set; }
}