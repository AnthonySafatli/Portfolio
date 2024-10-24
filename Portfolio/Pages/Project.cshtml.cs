using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Newtonsoft.Json;

namespace Portfolio.Pages;

public class ProjectModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet=true)]
    public string Name { get; set; }
    public Project? Project { get; set; }
    public ProjectPage? ProjectPage { get; set; }

    public ProjectModel(ProjectsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);

        if (Project == null)
            return NotFound();

        if (Project.Hidden)
            return NotFound();

        if (Project.PageContent == null)
            return NotFound();

        try
        {
            ProjectPage = JsonConvert.DeserializeObject<ProjectPage>(Project.PageContent);
        }
        catch (Exception)
        {
            return NotFound();
        }

        if (ProjectPage == null)
            return NotFound();

        return Page();
    }
}
