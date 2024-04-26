using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Upload;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    [BindProperty, Display(Name = "File to Upload")]
    public IFormFile File { get; set; }
    [BindProperty]
    public string FileLocation { get; set; }
    [BindProperty, Display(Name = "New Name")]
    public string Rename { get; set; }

    public IndexModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Rename");
        ModelState.Remove("FileLocation");
        if (!ModelState.IsValid)
            return Page();

        // TODO: More data validation

        string fileName = Rename == null ? File.FileName : Rename;
        string folder = FileLocation == null ? "" : FileLocation;
        string filePath = Path.Combine(_environment.WebRootPath, folder, fileName);
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await File.CopyToAsync(fileStream);

        return Redirect("/Admin/Dashboard");
    }
}
