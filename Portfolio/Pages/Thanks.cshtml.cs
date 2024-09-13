using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages
{
    public class ThanksModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (String.IsNullOrEmpty((string?)TempData["Email"]) || String.IsNullOrEmpty((string?)TempData["Message"]))
            {
                return Redirect("Contact");
            }
            else
            {
                return Page();
            }
        }
    }
}
