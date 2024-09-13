using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Pages;

public class ContactModel : PageModel
{
    // TODO: Do scam/spam filtering
    [BindProperty]
    public ContactMessage? Message { get; set; }

    public ContactModel()
    {
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (Message == null) {
            return Page();
        }
        // TODO: more input validation?

        // TODO: Replace css with actual css link (and test it)
        string subject = "New message! - anthonysafatli.com";
        string body = @"
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
    <link rel=""stylesheet"" href=""https://localhost:44358/css/admin.css"">
</head>
<body>
    <header>
    </header>

    <main>
        <h1>New Message</h1>
        <div class=""section center"">
            From: <a class=""inline-button"" href=""mailto:" + Message.Email + @""">" + Message.Email + @"</a>
        </div>
        <div class=""center section"">
            Message:
        </div>
        <div class=""section center"">
            " + Message.Message + @"
        </div>
    </main>
</body>
</html>";

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(Security.Config.FromAddress);
        mail.To.Add(Security.Config.ToAddress);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(Security.Config.FromAddress, Security.Config.FromAddressPassword);

        smtpClient.Send(mail);

        return Page();
    }
}
