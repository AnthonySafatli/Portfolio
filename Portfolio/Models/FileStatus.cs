namespace Portfolio.Models;

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

public class MarkDownStatus : FileStatus 
{
    public bool JsonStatus { get; set; }
    public bool MediaStatus { get; set; }

    public MarkDownStatus(string fullPath, string shortPath, bool used, bool jsonStatus, bool mediaStatus) 
        : base(fullPath, shortPath, used) 
    {
        JsonStatus = jsonStatus;
        MediaStatus = mediaStatus;
    }
}
