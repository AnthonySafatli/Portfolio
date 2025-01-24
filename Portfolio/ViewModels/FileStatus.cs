namespace Portfolio.ViewModels;

public class FileStatus
{
    public string FullPath { get; set; }
    public string ShortPath { get; set; }
    public bool Used { get; set; }

    public FileStatus(string fullPath, string shortPath, bool used)
    {
        FullPath = fullPath;
        ShortPath = shortPath;
        Used = used;
    }
}
