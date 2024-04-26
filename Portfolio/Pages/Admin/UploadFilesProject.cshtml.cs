using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Pages.Admin;

[Authorize]
public class UploadFilesProjectModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }
    public Project? Project { get; set; }
    public ProjectPage? ProjectPage { get; set; }
    public bool HasMd { get; set; } = false;
    public bool HasJson {  get; set; } = false;

    [BindProperty, Display(Name = "Markdown File")]
    public IFormFile MdFile { get; set; }

    public UploadFilesProjectModel(ProjectsContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Project = await _context.Projects.FirstOrDefaultAsync(x => x.Name == Name);

        if (Project == null)
            return NotFound(); 

        string mdPath = @"Projects\Markdown\" + Project.File + ".md";
        string jsonPath = @"Projects\Json\" + Project.File + ".json";
        if (!System.IO.File.Exists(mdPath))
            return Page();      // Upload Markdown
        HasMd = true;

        if (!System.IO.File.Exists(jsonPath))
            return Page();      // Rerun Script

        string jsonString = System.IO.File.ReadAllText(jsonPath);
        if (jsonString == null)
            return Page();      // Rerun Script
        HasJson = true;

        ProjectPage = JsonConvert.DeserializeObject<ProjectPage>(jsonString);
        return Page();          // Upload HyperText Links
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!HasMd)
        {
            return await UploadMdFile();
        } 
        else if (!HasJson)
        {
            return RunJsonScript();
        }
        else
        {
            return await UploadProjectFiles();
        }
    }

    public async Task<IActionResult> UploadMdFile()
    {
        // TODO: imput checking. make sure this is the right method to go to

        string filePath = _environment.ContentRootPath + Path.Combine("\\Projects\\Markdown\\", Project.File + ".md");
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await MdFile.CopyToAsync(fileStream);

        Project.RunMdToJson();

        return Redirect("/Admin/UploadFilesproject/" + Project.Name);
    }

    private IActionResult RunJsonScript()
    {
        throw new NotImplementedException();
    }

    private async Task<IActionResult> UploadProjectFiles()
    {
        throw new NotImplementedException();
    }
}
