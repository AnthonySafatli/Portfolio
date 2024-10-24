using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Projects;

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
    public Project Project { get; set; } = default!;
    [BindProperty]
    public IFormFile? PageContentFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (PageContentFile != null)
        {
            try
            {
                string pageContent = await Project.ExtractTextFromFileAsync(PageContentFile);
                Project.PageContent = pageContent;
            }
            catch (ArgumentException)
            {
                Project.PageContent = null;
            }

        }

        _context.Projects.Add(Project);
        await _context.SaveChangesAsync();

        return Redirect("../");
    }
}
