using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

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
            Project = new Project()
            {
                Name = Name,
                DateStarted = DateTime.MinValue,
                DateEnded = DateTime.Now,
                File = "C:\\Users\\Anthony\\source\\repos\\Portfolio\\Portfolio\\Projects\\Json\\test.json",
            };

            //return NotFound(); // remove for testing
        }

        if (!System.IO.File.Exists(Project.File)) // TODO: Change to file name, check json, if no json, check markdown, if markdown use python, otherwise, not found
        {
            return NotFound();
        }

        string jsonString = System.IO.File.ReadAllText(Project.File);

        if (jsonString == null)
        {
            return NotFound();
        }

        ProjectPage = JsonConvert.DeserializeObject<ProjectPage>(jsonString);

        return Page();
    }
}
