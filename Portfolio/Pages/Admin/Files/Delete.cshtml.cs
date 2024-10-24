using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    public DeleteModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [BindProperty(SupportsGet = true)]
    public string Path { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            string path = System.IO.Path.Combine(_environment.WebRootPath, "projects", HttpUtility.UrlDecode(Path));
            System.IO.File.Delete(path);
        }
        catch (Exception) { }

        return Redirect("../Index");
    }
}
