using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Projects;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ProjectsContext _context;

    public DeleteModel(ProjectsContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (Project.Name == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(Project.Name);
        if (project != null)
        {
            Project = project;
            _context.Projects.Remove(Project);
            await _context.SaveChangesAsync();
        }

        return Redirect("../");
    }
}
