using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Portfolio.Models;

public class Project
{
    public string Name { get; set; }
    public DateTime DateStarted { get; set; }   
    public DateTime DateEnded { get; set; }     
    public string File {  get; set; }
    public string Thumbnail { get; set; }
    public bool Hidden { get; set; }

    // TODO: Make description property

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
