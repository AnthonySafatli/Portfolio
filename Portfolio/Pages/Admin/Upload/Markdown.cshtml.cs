using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Upload;

[Authorize]
public class MarkdownModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }

    public bool HasMd { get; set; } = false;
    public bool HasJson { get; set; } = false;

    [BindProperty, Display(Name = "Markdown File")]
    public IFormFile MdFile { get; set; }

    public MarkdownModel(ProjectsContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Project == null)
        {
            Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);
            if (Project == null)
                return NotFound();
        }

        string filePath = _environment.ContentRootPath + Path.Combine("\\Projects\\Markdown\\", Project.File + ".md");
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await MdFile.CopyToAsync(fileStream);

        Project.RunMdToJson();

        return Redirect("/Admin/Upload/Markdown/" + Project.Name);
    }
}
