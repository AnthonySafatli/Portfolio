namespace Portfolio.Models;

public class PageElement
{
    public string Name { get; set; }
    public string? Text { get; set; }
    public int? Degree { get; set; }
    public bool? Ordered { get; set; }
    public string[]? Items { get; set; }
    public string? Link { get; set; }
}
