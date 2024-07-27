using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin.Files;

public class JsonModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string File { get; set; }

    public IActionResult OnGet()
    {
        // TODO: Run python script

        return Redirect("../Index");
    }
}
