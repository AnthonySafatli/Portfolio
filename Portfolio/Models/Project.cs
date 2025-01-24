using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Portfolio.Models;

public class Project
{
    public string Id { get; set; }
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; }

    [Display(Name = "Date Started")]
    [DataType(DataType.Date)]
    public DateTime DateStarted { get; set; }
    [Display(Name = "Date Ended")]
    [DataType(DataType.Date)]
    public DateTime DateEnded { get; set; }

    [Display(Name = "Page Content")]
    public string? PageContent {  get; set; }
    public string Thumbnail { get; set; }
    public string Description { get; set; }
    
    public bool Hidden { get; set; }
    [Display(Name = "Sort Order")]
    public int SortOrder { get; set; }
    
    public string? Github { get; set; }
    public string? Page { get; set; }
    public string? Download { get; set; }
}