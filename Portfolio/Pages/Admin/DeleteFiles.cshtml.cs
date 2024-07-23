using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin;

[Authorize]
public class DeleteFilesModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    public List<string> Files { get; set; }

    public DeleteFilesModel(ProjectsContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> OnGetAsync(string? file)
    {
        if (file != null)
        {
            string webRoot = _environment.WebRootPath;
            string fullPath = Path.Combine(webRoot, "projects") + file;
            try
            {
                System.IO.File.Delete(fullPath);
            } catch (Exception ex) { }
        }

        Files = GetAllFiles(); // TODO: Can you delete md and json files?
        IList<Project> projects = await _context.Projects.ToListAsync();

        foreach (Project proj in projects)
        {
            string jsonPath = @"Projects\Json\" + proj.File + ".json";
            if (!System.IO.File.Exists(jsonPath))
                continue;

            string jsonString = System.IO.File.ReadAllText(jsonPath);
            if (jsonString == null)
                continue;

            ProjectPage projectJson = JsonConvert.DeserializeObject<ProjectPage>(jsonString);
            if (projectJson == null)
                continue;

            foreach (PageElement elem in projectJson.Elements)
            {
                if (elem.Name == "media" && elem.Link != null)
                {
                    if (Files.Contains(elem.Link))
                    {
                        Files.Remove(elem.Link);
                    }
                }
            }

            // TODO: Check thumbnails as well
        }

        return Page();
    }

    private List<string> GetAllFiles()
    {
        string dir = Path.Combine(_environment.WebRootPath, "projects");
        List<string> files = GetAllFiles(dir);

        string rootDir = Path.Combine(_environment.WebRootPath, "projects");
        for (int i = 0; i < files.Count; i++)
        {
            files[i] = files[i].Remove(0, rootDir.Length);
        }

        return files;
    }

    private List<string> GetAllFiles(string rootDirectory)
    {
        List<string> files = new List<string>();

        files.AddRange(Directory.GetFiles(rootDirectory));
        foreach (string directory in Directory.GetDirectories(rootDirectory))
        {
            files.AddRange(GetAllFiles(directory));
        }

        return files;
    }
}
