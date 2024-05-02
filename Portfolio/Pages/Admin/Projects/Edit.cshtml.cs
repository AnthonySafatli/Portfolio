using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Projects;

[Authorize]
public class EditModel : PageModel
{
    private readonly ProjectsContext _context;

    public EditModel(ProjectsContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project =  await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        Project = project;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
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

        return Redirect("/Admin/Upload/Markdown/" + Project.Name);
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}
