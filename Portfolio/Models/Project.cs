using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Portfolio.Models;

public class Project
{
    public string Id { get; set; }
    public string DisplayName { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateStarted { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateEnded { get; set; }     
    
    public string? PageContent {  get; set; }
    public string Thumbnail { get; set; }
    public string Description { get; set; }
    
    public bool Hidden { get; set; }
    public int SortOrder { get; set; }
    
    public string? Github { get; set; }
    public string? Page { get; set; }
    public string? Download { get; set; }
}