using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages;

public class ProjectModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet=true)]
    public string Name { get; set; }
    public Project? Project { get; set; }

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
            };

            //return NotFound(); // remove for testing
        }

        return Page();
    }
}
