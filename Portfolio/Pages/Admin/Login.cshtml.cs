using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // TODO: Use database instead
        if (Credential.Username == "Anthony" && Credential.Password == "Safatli")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Credential.Username),
                new Claim(ClaimTypes.Email, "anthonysafatli@dal.ca")
            };

            var identity = new ClaimsIdentity(claims, "AdminCookieAuth"); // TODO: Use constants
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AdminCookieAuth", claimsPrincipal);

            return RedirectToPage("/Admin/Dashboard");
        }

        return Page();
    }
}

public class Credential
{
    [Required]
    [Display(Name = "User Name")]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
