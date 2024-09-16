using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ProjectsContext _context;

    public DashboardModel(ProjectsContext context)
    {
        _context = context;
    }

    public IList<Project> Project { get; set; }

    // TODO: rework entire admin system

    public async void OnGet()
    {
        Project = await _context.Projects.ToListAsync();
    }
}
