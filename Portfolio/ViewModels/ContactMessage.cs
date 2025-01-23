using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Portfolio.ViewModels;

public class ContactMessage
{
    public string Email { get; set; } = "";
    public string Message { get; set; } = "";
}
