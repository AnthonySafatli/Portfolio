using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Pages.Admin.Projects;

[Authorize]
public class ViewModel : PageModel
{
    private readonly ProjectsContext _context;

    public ViewModel(ProjectsContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    [BindProperty]
    public IFormFile? PageContentFile { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        Project = project;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Fetch the existing project to ensure it's being tracked
        var existingProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(e => e.Id == Project.Id);

        if (existingProject == null)
        {
            return NotFound();
        }

        if (PageContentFile != null)
        {
            try
            {
                string pageContent = await FileUtility.ExtractTextAsync(PageContentFile);
                Project.PageContent = pageContent;
            }
            catch (ArgumentException)
            {
                Project.PageContent = null;
            }
        }
        else
        {
            Project.PageContent = existingProject.PageContent;
        }

        _context.Attach(Project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(Project.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Redirect("../");
    }


    private bool ProjectExists(string id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}
