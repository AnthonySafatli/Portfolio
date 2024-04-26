using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Portfolio.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }            // TODO: Make unique
    public DateTime DateStarted { get; set; }   // TODO: Change to only date
    public DateTime DateEnded { get; set; }     // TODO: Change to only date
    public string File {  get; set; }
    public string Thumbnail { get; set; }

    public bool RunMdToJson()
    {
        string pythonInterpreter = "python";
        string pythonScript = @"Scripts\md_to_json.py " + File;

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
        using (Process process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }

        return string.IsNullOrEmpty(error);
    }
}
