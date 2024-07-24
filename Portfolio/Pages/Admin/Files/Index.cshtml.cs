using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ProjectsContext _context;
    private readonly IWebHostEnvironment _environment;

    public List<FileStatus> AssetFiles { get; set; } = new List<FileStatus>();
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

        string assetDir = Path.Combine(_environment.WebRootPath, "assets");
        List<string> assetFiles = GetAllFiles(assetDir);

        foreach (string asset in assetFiles)
        {
            string shortPath = asset.Remove(0, assetDir.Length);
            AssetFiles.Add(new FileStatus(asset, shortPath, true));
        }

        string projectDir = Path.Combine(_environment.WebRootPath, "projects");
        List<string> projectFiles = GetAllFiles(projectDir);
        List<string> usedProjectFiles = new List<string>();

        foreach (Project proj in projects) 
        {
            usedProjectFiles.Add(proj.Thumbnail);

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
                    usedProjectFiles.Add(elem.Link);
                }
            }
        }

        foreach (string projectFile in projectFiles)
        {
            string shortPath = projectFile.Remove(0, projectDir.Length);
            bool used = usedProjectFiles.Contains(shortPath);

            ProjectFiles.Add(new FileStatus(projectFile, shortPath, used));
        }

        string mdDir = Path.Combine(_environment.ContentRootPath, "Projects", "Markdown");
        List<string> mdFiles = GetAllFiles(projectDir);

        foreach (string mdFile in mdFiles)
        {
            string shortPath = mdFile.Remove(0, mdDir.Length);
            bool used = projects.Any(p => "Projects/Markdown/" + p.File + ".md" == shortPath);
            bool jsonStatus = true; // TODO: Fix later

            MarkDownFiles.Add(new MarkDownStatus(mdFile, shortPath, used, jsonStatus));
        }
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
