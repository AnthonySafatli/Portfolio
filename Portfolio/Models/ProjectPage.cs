
namespace Portfolio.Models;

public class ProjectPage
{
    public PageElement[] Elements {  get; set; }
}

public class PageElement
{
    public string Name { get; set; }
    public string? Text { get; set; }
    public int? Degree { get; set; }
    public bool? Ordered { get; set; }
    public string[]? Items { get; set; }
    public string? Link { get; set; }
    
    public string ToHTML()
    {
        switch (Name)
        {
            case "header":
                return $"<h{Degree}>{Text}</h{Degree}>";
            
            case "quote":
                return $"<blockquote>{Text}</blockquote>";
            
            case "list":
                string tag = (bool)Ordered ? "ol" : "ul";

                string list = $"<{tag}>";
                foreach (string item in Items)
                {
                    list += item;
                }
                list += $"</{tag}>";

                return tag;
            
            case "media":
                return "";

            case "link":
                return $"<a href={Link}>{Text}</a>";

            case "horizontal":
                return "<hr>";

            case "code":
                string code = "<code><pre>";
                foreach (string item in Items)
                {
                    code += "\n" + item;
                }
                code += "</pre></code>";

                return code;

            case "paragraph":
                return $"<p>{Text}</p>";

        }

        return "";
    }

    public override string ToString() => ToHTML();
}