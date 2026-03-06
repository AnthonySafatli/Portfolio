using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Services;
using Portfolio.ViewModels;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Pages;

public class ContactModel : PageModel
{
    private readonly PortfolioDbContext _portfolioDbContext;
    private readonly LocationService _locationService;

    // TODO: Do scam/spam filtering
    [BindProperty]
    public ContactMessage? Message { get; set; }

    public ContactModel(PortfolioDbContext portfolioDbContext, LocationService locationService)
    {
        _portfolioDbContext = portfolioDbContext;
        _locationService = locationService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (Message == null || String.IsNullOrEmpty(Message.Message) || String.IsNullOrEmpty(Message.Email)) 
        {
            return Page();
        }

        var dbMessage = new Message()
        {
            Email = Message.Email,
            Text = Message.Message,

            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
            UserAgent = Request.Headers["User-Agent"].ToString(),
            Referer = Request.Headers["Referer"].ToString(),
            Location = await _locationService.GetLocationAsync(HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""),
            
            SubmissionTime = DateTime.UtcNow,
            Status = MessageStatus.New
        };

        _portfolioDbContext.Messages.Add(dbMessage);
        await _portfolioDbContext.SaveChangesAsync();

        // TODO: Send email to site owner

        TempData["Email"] = Message.Email;
        TempData["Message"] = Message.Message;

        return RedirectToPage("Thanks");
    }
}
