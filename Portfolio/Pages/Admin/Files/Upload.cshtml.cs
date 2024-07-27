using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class AddModel : PageModel
{
    public void OnGet()
    {
    }
}
