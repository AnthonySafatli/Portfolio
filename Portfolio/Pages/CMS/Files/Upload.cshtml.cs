using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.CMS.Files;

[Authorize]
public class UploadModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    [BindProperty, Display(Name = "File to Upload")]
    public IFormFile File { get; set; }
    [BindProperty]
    public string FileLocation { get; set; }
    [BindProperty, Display(Name = "New Name")]
    public string Name { get; set; }

    public UploadModel(IWebHostEnvironment environment)
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

        string fileName = (Name == null) ? File.FileName : Name;
        string folder = (FileLocation == null) ? "" : FileLocation;
        string filePath = Path.Combine(_environment.WebRootPath, "projects", folder, fileName);
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await File.CopyToAsync(fileStream);

        // TODO: Create folder if folder doesnt exist

        return Redirect("./Index");
    }
}