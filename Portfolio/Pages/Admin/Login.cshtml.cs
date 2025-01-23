using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Portfolio.Pages.Admin;

public class LoginModel : PageModel
{
    private readonly EmailService _email;

    [BindProperty]
    public Credential Credential { get; set; }

    public LoginModel(EmailService email)
    {
        _email = email;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (SecurityService.EncryptSHA256(Credential.Password) == SecurityService.Config.AdminPassword)
        {
            bool valid = await _email.loginAlert(HttpContext, "New Login!");
            
            if (valid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Anthony"),
                };

                var identity = new ClaimsIdentity(claims, SecurityService.Config.AdminCookieName);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(SecurityService.Config.AdminCookieName, claimsPrincipal, authProperties);

                return RedirectToPage("/Admin/Dashboard");
            }
        }

        await _email.loginAlert(HttpContext, "Failed Login Attempt!");

        return Page();
    }
}

public class Credential
{
    [Required]
    public string Password { get; set; }
}
