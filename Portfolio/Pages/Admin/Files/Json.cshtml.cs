using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Files;


[Authorize]
public class JsonModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    [BindProperty, Display(Name = "File to Upload")]
    public IFormFile File { get; set; }
    [BindProperty, Display(Name = "File Name")]
    public string Name { get; set; }

    public JsonModel(IWebHostEnvironment environment)
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

        // TODO: More data validation

        string filePath = Path.Combine(_environment.WebRootPath, "assets/project_json", Name + ".json");
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await File.CopyToAsync(fileStream);

        // TODO: Create folder if folder doesnt exist

        return Redirect("./Index");
    }
}