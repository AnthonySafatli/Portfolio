using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin.Files;

public class DeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Path { get; set; }

    public IActionResult OnGet()
    {
        //TODO: delete "path" (md and json)

        return Redirect("../Index");
    }
}
