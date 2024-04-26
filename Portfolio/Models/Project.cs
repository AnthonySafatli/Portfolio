using System.Diagnostics;

namespace Portfolio.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateEnded { get; set; }
    public string File {  get; set; }
    public string Thumbnail { get; set; }

    public void RunMdToJson()
    {
        string pythonInterpreter = "python";
        string pythonScript = @"Utilities\md_to_json.py " + File;

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
        using (Process process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }
    }
}
