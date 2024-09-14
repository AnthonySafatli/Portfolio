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
        ViewData["Email"] = Security.Config.ToAddress;
    }

    public IActionResult OnPost()
    {
        if (Message == null) 
        {
            return Page();
        } 
        else
        {
            if (String.IsNullOrEmpty(Message.Message) || String.IsNullOrEmpty(Message.Email))
            {
                return Page();
            }
        }

        string subject = "New message! - anthonysafatli.ca";
        string body = @"
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
</head>
<body>
    <header>
    </header>

    <main>
        <h1>New Message</h1>
        <div>
            <p>From: <a class=""inline-button"" href=""mailto:" + Message.Email + @""">" + Message.Email + @"</a></p>
        </div>
        <div>
            <p>Message: " + Message.Message + @"</p>
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

        TempData["Email"] = Message.Email;
        TempData["Message"] = Message.Message;
        return RedirectToPage("Thanks");
    }
}
