using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Portfolio.Models;

public class Project
{
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateStarted { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateEnded { get; set; }     
    
    public string? PageContent {  get; set; }
    public string Thumbnail { get; set; }
    public string Description { get; set; }
    
    public bool Hidden { get; set; }
    
    public string? Github { get; set; }
    public string? Page { get; set; }
    public string? Download { get; set; }
    public string? Tags { get; set; }

    public static async Task<string> ExtractTextFromFileAsync(IFormFile file)
    {
        if (file == null)
            throw new ArgumentException("No file was uploaded.");

        if (file.Length == 0)
            throw new ArgumentException("The file is empty.");

        // Read the file content
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            if (stream.Length == 0)
                throw new ArgumentException("The uploaded file contains no data.");

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string fileContent = await reader.ReadToEndAsync();
                return fileContent;
            }
        }
    }
}