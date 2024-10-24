namespace Portfolio.Models;

public class Project
{
    public string Name { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateEnded { get; set; }     
    public string? PageContent {  get; set; }
    public string Thumbnail { get; set; }
    public bool Hidden { get; set; }
    public string Description { get; set; }
    public string? Github { get; set; }
    public string? Page { get; set; }
    public string? Download { get; set; }
    public string? Tags { get; set; }
}
