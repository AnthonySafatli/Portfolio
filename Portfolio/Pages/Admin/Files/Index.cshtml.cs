using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;
using System.Diagnostics;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    public List<FileStatus> ProjectFiles { get; set; } = new List<FileStatus>();
    public List<MarkDownStatus> MarkDownFiles { get; set; } = new List<MarkDownStatus>();

    public IndexModel(ProjectsContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async void OnGet()
    {
        IList<Project> projects = await _context.Projects.ToListAsync();

        string projectDir = Path.Combine(_environment.WebRootPath, "projects");
        List<string> projectFiles = GetAllFiles(projectDir);
        List<string> usedProjectFiles = GetUsedFiles(projects);

        foreach (string projectFile in projectFiles)
        {
            string shortPath = projectFile.Remove(0, projectDir.Length + 1).Replace("\\", "/");
            bool used = usedProjectFiles.Contains(shortPath) || usedProjectFiles.Contains("/" + shortPath);

            ProjectFiles.Add(new FileStatus(projectFile, shortPath, used));
        }
    }

    private List<string> GetUsedFiles(IList<Project> projects)
    {
        List<string> usedProjectFiles = new List<string>();

        foreach (Project proj in projects)
        {
            usedProjectFiles.Add(proj.Thumbnail);

            if (proj.PageContent == null)
                continue;

            ProjectPage? projectJson = null;
            try
            {
                projectJson = JsonConvert.DeserializeObject<ProjectPage>(proj.PageContent);
            }
            catch (Exception)
            {
                continue;
            }

            if (projectJson == null)
                continue;

            foreach (PageElement elem in projectJson.Elements)
            {
                if (elem.Name == "media" && elem.Link != null)
                {
                    usedProjectFiles.Add(elem.Link);
                }
            }
        }

        return usedProjectFiles;
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
