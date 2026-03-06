namespace Portfolio.Models;

public class Message
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string Referer { get; set; }
    public string Location { get; set; }

    public DateTime SubmissionTime { get; set; }
    public MessageStatus Status{ get; set; }
}

public enum MessageStatus
{
    New         = 0,
    Read        = 1,
    Archived    = 2,
    Deleted     = 3
}