using System.Text;

namespace Portfolio.Utilities;

public static class FileUtility
{
    public static async Task<string> ExtractTextAsync(IFormFile file)
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
