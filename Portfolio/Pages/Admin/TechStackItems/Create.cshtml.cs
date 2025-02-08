using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Pages.Admin.TechStackItems;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ProjectsContext _context;

    public CreateModel(ProjectsContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public TechStackItem TechStackItem { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.TechStackItems.Add(TechStackItem);
        await _context.SaveChangesAsync();

        return Redirect("../");
    }
}
