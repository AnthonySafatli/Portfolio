using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Projects;

public class IndexModel : PageModel
{
    private readonly Portfolio.Data.ProjectsContext _context;

    public IndexModel(Portfolio.Data.ProjectsContext context)
    {
        _context = context;
    }

    public IList<Project> Project { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Project = await _context.Projects.ToListAsync();
    }
}
