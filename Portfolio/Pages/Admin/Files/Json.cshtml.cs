using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class JsonModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string File { get; set; }

    public IActionResult OnGet()
    {
        string pythonInterpreter = "python";
        string pythonScript = @"Scripts\md_to_json.py " + File.Substring(0, File.Length - 3);

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

        return Redirect("../Index");
    }
}
