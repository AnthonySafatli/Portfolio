using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models;

public class TechStackItem
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    [Display(Name = "Path To Image")]
    public string PathToImage { get; set; }

    public ICollection<Project> Projects { get; set; } = [];
}
