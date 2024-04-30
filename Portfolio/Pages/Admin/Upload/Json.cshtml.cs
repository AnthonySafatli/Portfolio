using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Upload;

[Authorize]
public class JsonModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }

    public bool HasMd { get; set; } = false;
    public bool HasJson { get; set; } = false;


    public JsonModel(ProjectsContext context, IWebHostEnvironment environment)
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
        HasMd = System.IO.File.Exists(mdPath);
        HasJson = System.IO.File.Exists(jsonPath);

        if (HasJson)
        {
            string jsonString = System.IO.File.ReadAllText(jsonPath);
            if (jsonString == null)
                HasJson = false;
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Project == null)
        {
            Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);
            if (Project == null)
                return NotFound();
        }

        Project.RunMdToJson();

        return Redirect("/Admin/ProjectFiles/" + Project.Name);
    }
}
