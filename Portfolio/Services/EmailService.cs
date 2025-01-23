using Newtonsoft.Json.Linq;
using Portfolio.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Portfolio.Services;

public class EmailService
{
    public async Task sendEmail(string subject, string body)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(SecurityService.Config.FromAddress));
        email.To.Add(MailboxAddress.Parse(SecurityService.Config.ToAddress));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("m01.internetmailserver.net", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(SecurityService.Config.FromAddress, SecurityService.Config.FromAddressPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task sendContactMessage(string email, string message)
    {
        string subject = "New message! - anthonysafatli.ca";
        string body = "From: " + email + "\n\nMessage:\n" + message;
        await sendEmail(subject, body);
    }

    public async Task<bool> loginAlert(HttpContext http, string message)
    {
        string subject = message + " - anthonysafatli.ca";
        string? userIpAddress = http.Connection.RemoteIpAddress?.ToString();
        string loginAttemptTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string? userAgent = http.Request.Headers["User-Agent"];
        string location = userIpAddress == null ? "" : await GetLocation(userIpAddress);

        string body = $"Time: {loginAttemptTime}\n" +
                      $"IP Address: {userIpAddress}\n" +
                      $"Location: {location}\n" +
                      $"User-Agent: {userAgent}";

        await sendEmail(subject, body);

        if (userIpAddress == null || userAgent == null)
            return false;
        else
            return true;
    }

    private async Task<string> GetLocation(string ipAddress)
    {
        using HttpClient client = new HttpClient();
        string url = $"https://ipinfo.io/{ipAddress}/json";
        var response = await client.GetStringAsync(url);
        var json = JObject.Parse(response);
        string location = $"{json["city"]}, {json["region"]}, {json["country"]}";

        return location;
    }
}
