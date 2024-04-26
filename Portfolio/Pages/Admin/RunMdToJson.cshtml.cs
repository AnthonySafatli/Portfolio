using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin;

public class RunMdToJsonModel : PageModel
{
    private readonly ProjectsContext _context;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }
    public ProjectPage? ProjectPage { get; set; }

    public RunMdToJsonModel(ProjectsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);

        if (Project == null)
            return NotFound();

        Project.RunMdToJson();

        return Redirect("/Admin/UploadFilesProject/" + Name);
    }
}
