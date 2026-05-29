using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages.Admin;

[Authorize]
public class LogoutModel : PageModel
{
    private readonly string _adminCookieName;

    public LogoutModel(IConfiguration config)
    {
        _adminCookieName = config["AdminCookieName"]!;
    }

    public IActionResult OnGet()
    {
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await HttpContext.SignOutAsync(_adminCookieName);
        return RedirectToPage("/Index");
    }
}
