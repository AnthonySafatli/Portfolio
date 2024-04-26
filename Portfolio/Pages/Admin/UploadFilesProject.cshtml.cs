using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin;

[Authorize]
public class UploadFilesProjectModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }
    public ProjectPage? ProjectPage { get; set; }
    public bool HasMd { get; set; } = false;
    public bool HasJson {  get; set; } = false;

    public UploadFilesProjectModel(ProjectsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);

        if (Project == null)
            return NotFound(); 

        string mdPath = @"Projects\Markdown\" + Project.File + ".md";
        string jsonPath = @"Projects\Json\" + Project.File + ".json";
        if (!System.IO.File.Exists(mdPath))
            return Page();      // Upload Markdown
        HasMd = true;

        if (!System.IO.File.Exists(jsonPath))
            return Page();      // Rerun Script

        string jsonString = System.IO.File.ReadAllText(jsonPath);
        if (jsonString == null)
            return Page();      // Rerun Script
        HasJson = true;

        ProjectPage = JsonConvert.DeserializeObject<ProjectPage>(jsonString);
        return Page();          // Upload HyperText Links
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return Page();
    }
}
