using Portfolio.Models;

namespace Portfolio.Services;

public class PageRenderingService
{
    private static readonly string[] ImageExt = { "jpg", "jpeg", "svg", "gif", "bmp", "png", "webp" };
    private static readonly string[] VideoExt = { "mp4", "ogg", "webm" };

    public string RenderElement(PageElement element)
    {
        switch (element.Name)
        {
            case "header":
                return $"\n<h{element.Degree}>{element.Text}</h{element.Degree}>\n";

            case "quote":
                return $"\n<blockquote>{element.Text}</blockquote>\n";

            case "list":
                string tag = element.Ordered == true ? "ol" : "ul";
                string list = $"\n<{tag}>";
                foreach (string item in element.Items ?? Array.Empty<string>())
                {
                    list += "\n<li>" + item + "</li>";
                }
                list += $"\n</{tag}>\n";
                return list;

            case "media":
                string extension = Path.GetExtension(element.Link)?.TrimStart('.').ToLower() ?? "";
                string classes = element.Bordered == true ? "bordered" : "";

                if (ImageExt.Contains(extension))
                {
                    return $"\n<div class=\"media-div\"><img class=\"{classes}\" src=\"/projects{element.Link}\" alt=\"{element.Text}\" loading=\"lazy\"></div>\n";
                }
                else if (VideoExt.Contains(extension))
                {
                    return $"\n<div class=\"media-div\"><video class=\"{classes}\" controls><source src=\"/projects{element.Link}\" type=\"video/{extension}\">{element.Text}</video></div>\n";
                }
                return "";

            case "link":
                return $"\n<a href=\"{element.Link}\" target=\"_blank\">{element.Text}</a>\n";

            case "horizontal":
                return "\n<hr>\n";

            case "code":
                string code = "\n<code><pre>";
                foreach (string item in element.Items ?? Array.Empty<string>())
                {
                    code += "\n" + item;
                }
                code += "</pre></code>\n";
                return code;

            case "paragraph":
                return $"\n<p>{element.Text}</p>\n";

            default:
                return "";
        }
    }
}
