﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Projects;

public class DetailsModel : PageModel
{
    private readonly Portfolio.Data.ProjectsContext _context;

    public DetailsModel(Portfolio.Data.ProjectsContext context)
    {
        _context = context;
    }

    public Project Project { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
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
        else
        {
            Project = project;
        }
        return Page();
    }
}