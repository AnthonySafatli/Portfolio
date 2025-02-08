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

namespace Portfolio.Pages.Admin.TechStackItems;

[Authorize]
public class ViewModel : PageModel
{
    private readonly ProjectsContext _context;

    public ViewModel(ProjectsContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TechStackItem TechStackItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var techStackItem = await _context.TechStackItems.FirstOrDefaultAsync(m => m.Id == id);
        if (techStackItem == null)
        {
            return NotFound();
        }

        TechStackItem = techStackItem;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Fetch the existing project to ensure it's being tracked
        var existingTechStackItem = await _context.TechStackItems.AsNoTracking().FirstOrDefaultAsync(e => e.Id == TechStackItem.Id);

        if (existingTechStackItem == null)
        {
            return NotFound();
        }

        _context.Attach(TechStackItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TechStackItemExists(TechStackItem.Id))
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


    private bool TechStackItemExists(int id)
    {
        return _context.TechStackItems.Any(e => e.Id == id);
    }
}
