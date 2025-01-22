using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages;

public class ProjectsModel : PageModel
{
    private readonly ProjectsContext _context;

    public List<Project> Projects { get; set; } = new();

    public ProjectsModel(ProjectsContext context)
    {
        _context = context;
    }

    public async void OnGetAsync()
    {
        Projects = await _context.Projects
            .Where(p => !p.Hidden)
            .OrderBy(p => p.SortOrder)
            .ToListAsync();
    }
}
