using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin.Upload;

public class ProjectFilesModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }

    public bool HasMd { get; set; } = false;
    public bool HasJson { get; set; } = false;

    [BindProperty]
    public List<PageMediaLink> Links { get; set; }

    public ProjectFilesModel(ProjectsContext context, IWebHostEnvironment environment)
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

        string? jsonString = null;
        if (HasJson)
        {
            jsonString = System.IO.File.ReadAllText(jsonPath);
            if (jsonString == null)
                HasJson = false;
        }

        if (HasJson)
        {
            Links = new();

            ProjectPage projectJson = JsonConvert.DeserializeObject<ProjectPage>(jsonString);
            if (projectJson == null)
                return NotFound();

            foreach (PageElement item in projectJson.Elements)
            {  
                if (item.Name == "media" && item.Link != null)
                {
                    if (AlreadyContainsLink(item.Link))
                        continue;

                    string path = Path.Combine(_environment.WebRootPath, "projects", item.Link);
                    if (!System.IO.File.Exists(path))
                    {
                        Links.Add(new PageMediaLink { Path = item.Link });
                    }
                }
            }
        }

        return Page();
    }

    private bool AlreadyContainsLink(string path)
    {
        foreach (PageMediaLink link in Links)
        {
            if (link.Path == path)
                return true;
        }

        return false;
    }

    public async Task<IActionResult> OnPost()
    {
        // TODO: More data validation

        if (ModelState.IsValid)
        {
            foreach (PageMediaLink link in Links)
            {
                string filePath = Path.Combine(_environment.WebRootPath, "projects", link.Path);
                string? directory = Path.GetDirectoryName(filePath);
                if (directory != null)
                    Directory.CreateDirectory(directory);

                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await link.File.CopyToAsync(fileStream);
            }
        }

        return Redirect("/Admin/Dashboard");
    }
}
