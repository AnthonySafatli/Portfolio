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

namespace Portfolio.Pages.Admin.TechStackItems;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ProjectsContext _context;

    public DeleteModel(ProjectsContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TechStackItem TechStackItem { get; set; } = default!;

    public IActionResult OnGet()
    {
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var techStackItem = await _context.TechStackItems.FindAsync(TechStackItem.Id);
        if (techStackItem != null)
        {
            TechStackItem = techStackItem;
            _context.TechStackItems.Remove(TechStackItem);
            await _context.SaveChangesAsync();
        }

        return Redirect("../");
    }
}
