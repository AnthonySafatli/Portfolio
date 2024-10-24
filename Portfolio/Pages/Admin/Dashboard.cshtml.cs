using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;
using System.ComponentModel;

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

    public async void OnGet()
    {
        Project = await _context.Projects.ToListAsync();
    }

    public bool validPageContent(string? pageContent)
    {
        if (string.IsNullOrEmpty(pageContent))
            return false;

        ProjectPage? page = null;
        try
        {
            page = JsonConvert.DeserializeObject<ProjectPage>(pageContent);
        }
        catch (Exception)
        {
            return false;
        }

        if (page == null)
            return false;

        return true;
    }
}
