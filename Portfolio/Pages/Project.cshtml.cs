using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel;

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

        Console.WriteLine(Project.File);
        if (!System.IO.File.Exists(Project.File))
        {
            return NotFound();
        }

        string jsonString = System.IO.File.ReadAllText(Project.File);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        
        ProjectPage = JsonSerializer.Deserialize<ProjectPage>(jsonString, options);

        return Page();
    }
}
