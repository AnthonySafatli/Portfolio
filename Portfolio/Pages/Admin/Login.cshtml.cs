using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;
using Portfolio.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Portfolio.Pages.Admin;

public class LoginModel : PageModel
{
    [BindProperty]
    public Credential Credential { get; set; }

    private readonly string _adminCookieName;
    private readonly string _adminPassword;

    public LoginModel(IConfiguration config)
    {
        _adminCookieName = config["AdminCookieName"]!;
        _adminPassword = config["AdminPassword"]!;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (SecurityUtility.EncryptSHA256(Credential.Password) == _adminPassword)
        {
            // bool valid = await _email.loginAlert(HttpContext, "New Login!");
            // TODO: Add a login alert for successful login

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Anthony"),
            };

            var identity = new ClaimsIdentity(claims, _adminCookieName);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(_adminCookieName, claimsPrincipal, authProperties);

            return RedirectToPage("/Admin/Dashboard");
        }

        // await _email.loginAlert(HttpContext, "Failed Login Attempt!");
        // TODO: Add a login alert for failed login 

        return Page();
    }
}

public class Credential
{
    [Required]
    public string Password { get; set; }
}
