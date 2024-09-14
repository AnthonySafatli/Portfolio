using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Portfolio.Pages.CMS;

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

        if (Security.EncryptSHA256(Credential.Password) == Security.Config.AdminPassword)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Anthony"),
            };

            var identity = new ClaimsIdentity(claims, Security.Config.AdminCookieName);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(Security.Config.AdminCookieName, claimsPrincipal, authProperties);

            return RedirectToPage("/CMS/Dashboard");
        }

        // TODO: Validation for wrong password
        return Page();
    }
}

public class Credential
{
    [Required]
    public string Password { get; set; }
}
