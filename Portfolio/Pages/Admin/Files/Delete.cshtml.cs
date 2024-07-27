using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin.Files;

public class DeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Path { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            System.IO.File.Delete(Path);
        }
        catch (Exception ex) { }

        return Redirect("../Index");
    }
}
