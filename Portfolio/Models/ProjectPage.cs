namespace Portfolio.Models;

public class ProjectPage
{
    public PageElement[] Elements {  get; set; }
}

public class PageElement
{
    private static string[] ImageExt = { "jpg", "jpeg", "svg", "gif", "bmp", "png", "webp" };
    private static string[] VideoExt = { "mp4", "ogg", "webm" };

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
                return $"\n<h{Degree}>{Text}</h{Degree}>\n";
            
            case "quote":
                return $"\n<blockquote>{Text}</blockquote>\n";
            
            case "list":
                string tag = (bool)Ordered ? "ol" : "ul";

                string list = $"\n<{tag}>";
                foreach (string item in Items)
                {
                    list += item;
                }
                list += $"</{tag}>\n";

                return tag;
            case "media":
                string extension = Path.GetExtension(Link).TrimStart('.').ToLower();

                if (Array.Exists(ImageExt, ext => ext == extension))
                {
                    return $"\n<div class=\"media-div\"><img src=\"/projects/{Link}\" alt=\"{Text}\"></div>\n";
                }
                else if (Array.Exists(VideoExt, ext => ext == extension))
                {
                    return $"\n<div class=\"media-div\"><video controls><source src=\"/projects/{Link}\" type=\"video/{extension}\">{Text}</video></div>\n";
                }

                return "";

            case "link":
                return $"\n<a href=\"https://{Link}\" target=\"_blank\">{Text}</a>\n";

            case "horizontal":
                return "\n<hr>\n";

            case "code":
                string code = "\n<code><pre>";
                foreach (string item in Items)
                {
                    code += "\n" + item;
                }
                code += "</pre></code>\n";

                return code;

            case "paragraph":
                return $"\n<p>{Text}</p>\n";

        }

        return "";
    }

    public override string ToString() => ToHTML();
}

public class PageMediaLink
{
    public string Path { get; set; }
    public IFormFile File { get; set; }
}