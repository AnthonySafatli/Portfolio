using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Portfolio.Pages.Admin;

public class LoginModel : PageModel
{
    [BindProperty]
    public Credential Credential { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (Security.EncryptSHA256(Credential.Password) == Security.AdminPassword)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Anthony"),
            };

            var identity = new ClaimsIdentity(claims, Security.AdminCookieName);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(Security.AdminCookieName, claimsPrincipal, authProperties);

            return RedirectToPage("/Admin/Dashboard");
        }

        return Page();
    }
}

public class Credential
{
    [Required]
    public string Password { get; set; }
}
