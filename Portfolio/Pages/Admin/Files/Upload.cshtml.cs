using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class UploadModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    [BindProperty, Display(Name = "File to Upload")]
    public IFormFile File { get; set; }
    [BindProperty]
    public string? FileLocation { get; set; }
    [BindProperty, Display(Name = "New Name")]
    public string? Name { get; set; }

    public UploadModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        string fileName = string.IsNullOrEmpty(Name) ? File.FileName : Name;
        string folder = string.IsNullOrEmpty(FileLocation) ? "" : FileLocation;
        string folderPath = Path.Combine(_environment.WebRootPath, "projects", folder);
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await File.CopyToAsync(fileStream);

        return Redirect("./Index");
    }
}