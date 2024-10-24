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
            bool used = usedProjectFiles.Contains(shortPath);

            ProjectFiles.Add(new FileStatus(projectFile, shortPath, used));
        }

        string mdDir = Path.Combine(_environment.ContentRootPath, "Projects", "Markdown");
        List<string> mdFiles = GetAllFiles(mdDir);

        foreach (string mdFile in mdFiles)
        {
            string shortPath = mdFile.Remove(0, mdDir.Length + 1).Replace("\\", "/");
            bool used = projects.Any(p => p.PageContent + ".md" == shortPath);
            bool? jsonStatus = CheckJson(shortPath);
            bool mediaStatus = CheckMedia(shortPath, projectFiles);

            MarkDownFiles.Add(new MarkDownStatus(mdFile, shortPath, used, jsonStatus, mediaStatus));
        }
    }

    private List<string> GetUsedFiles(IList<Project> projects)
    {
        List<string> usedProjectFiles = new List<string>();

        foreach (Project proj in projects)
        {
            usedProjectFiles.Add(proj.Thumbnail);

            string jsonPath = @"Projects\Json\" + proj.PageContent + ".json";
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

    private bool? CheckJson(string mdPath)
    {
        string pythonInterpreter = "python";
        string pythonScript = @"Scripts\md_to_json_tester.py " + mdPath.Substring(0, mdPath.Length - 3);

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonInterpreter,
            Arguments = pythonScript,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Create and start the process
        string error;
        string output;
        using (Process process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();

            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }

        if (!String.IsNullOrEmpty(error))
        {
            return null;
        }

        string filePath = Path.Combine(_environment.ContentRootPath, "Projects\\Json\\" + mdPath.Substring(0, mdPath.Length - 2) + "json");

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? firstLine = reader.ReadLine();

                if (firstLine == null)
                {
                    return false;
                } 
                else
                {
                    firstLine = firstLine.TrimEnd();
                    output = output.Replace('\'', '\"').TrimEnd();
                    return firstLine == output;
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private bool CheckMedia(string mdPath, List<string> projectFiles)
    {
        string jsonPath = @"Projects\Json\" + mdPath.Substring(0, mdPath.Length - 3) + ".json";
        if (!System.IO.File.Exists(jsonPath))
            return true;

        string jsonString = System.IO.File.ReadAllText(jsonPath);
        if (jsonString == null)
            return true;

        ProjectPage projectJson = JsonConvert.DeserializeObject<ProjectPage>(jsonString);

        if (projectJson == null)
            return true;

        foreach (PageElement elem in projectJson.Elements)
        {
            if (elem.Name == "media" && elem.Link != null)
            {
                if (!projectFiles.Contains(elem.Link))
                    return false;
            }
        }

        return true;
    }
}
