using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Pages;

public class ContactModel : PageModel
{
    private readonly EmailService _email;

    // TODO: Do scam/spam filtering
    [BindProperty]
    public EmailMessage? Message { get; set; }

    public ContactModel(EmailService email)
    {
        _email = email;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
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

        await _email.sendContactMessage(Message.Email, Message.Message);

        TempData["Email"] = Message.Email;
        TempData["Message"] = Message.Message;
        return RedirectToPage("Thanks");
    }
}
