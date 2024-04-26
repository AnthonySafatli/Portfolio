using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Portfolio.Pages;

public class ProjectModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet=true)]
    public string Name { get; set; }
    public Project? Project { get; set; }
    public ProjectPage ProjectPage { get; set; }

    public ProjectModel(ProjectsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);

        if (Project == null)
        {
            return NotFound(); 
        }

        // TODO: Change to file name, check json, if no json, check markdown, if markdown use python, otherwise, not found
        if (!System.IO.File.Exists(@"Projects\Json\" + Project.File + ".json"))
        {
            if (System.IO.File.Exists(@"Projects\Markdown\" + Project.File + ".md"))
            {
                Project.RunMdToJson();
            } 
            else
            {
                return NotFound();
            }
        }

        string jsonString = System.IO.File.ReadAllText(@"Projects\Json\" + Project.File + ".json");

        if (jsonString == null)
        {
            return NotFound();
        }

        ProjectPage = JsonConvert.DeserializeObject<ProjectPage>(jsonString);

        return Page();
    }
}
