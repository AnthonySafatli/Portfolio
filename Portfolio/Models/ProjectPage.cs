using System.Text.Json.Serialization;

namespace Portfolio.Models;

public class ProjectPage
{
    public PageElement[] Elements {  get; set; }
}

public abstract class PageElement
{
    public string Name { get; set; }
    public abstract string ToHTML();
}

public class HeaderElement : PageElement
{
    public int Degree { get; set; }
    public string Text { get; set; }

    public override string ToHTML()
    {
        return $"<h{Degree}>{Text}</h{Degree}>";
    }
}

public class QuoteElement : PageElement
{
    public string Text { get; set; }

    public override string ToHTML()
    {
        return $"<blockquote>{Text}</blockquote>";
    }
}

public class ListElement : PageElement
{
    public bool Ordered { get; set; }
    public string[] Items { get; set; }

    public override string ToHTML()
    {
        string tag = Ordered ? "ol" : "ul";

        string html = $"<{tag}>";
        foreach (string item in Items)
        {
            html += $"<li>{item}</li>";
        }
        html += "</{tag}>";

        return html;
    }
}

public class MediaElement : PageElement
{
    [JsonPropertyName("alt")]
    public string AltText { get; set; }
    [JsonPropertyName("file")]
    public string FilePath { get; set; }

    public override string ToHTML()
    {
        throw new NotImplementedException();
    }
}

public class LinkElement : PageElement
{
    [JsonPropertyName("alt")]
    public string AltText { get; set; }
    public string Link { get; set; }

    public override string ToHTML()
    {
        return $"<a href=\"{Link}\">{AltText}</a>";
    }
}

public class HorizontalElement : PageElement
{
    public override string ToHTML()
    {
        return "<hr>";
    }
}

public class CodeElemenet : PageElement
{
    [JsonPropertyName("lang")]
    public string Language { get; set; }
    public string[] Text { get; set; }

    public override string ToHTML()
    {
        string html = "<code><pre>";
        foreach (string item in Text)
        {
            html += "\n" + item;
        }
        html += "</pre></code>";

        return html;
    }
}

public class ParagraphElement : PageElement
{
    public string Text { get; set; }

    public override string ToHTML()
    {
        return $"<p>{Text}</p>";
    }
}