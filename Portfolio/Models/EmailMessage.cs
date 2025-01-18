using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Portfolio.Models;

public class EmailMessage
{
    public string Email { get; set; } = "";
    public string Message { get; set; } = "";
}
