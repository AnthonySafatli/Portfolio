using Newtonsoft.Json;

namespace Portfolio.Models;

public class PageElement
{
    public PageElementType Type { get; set; }
    
    public string? Data { get; set; }
    
    public string? Text { get; set; }

    [JsonProperty("sub_elements")]
    public PageElement[] SubElements { get; set; }
}

public enum PageElementType
{
    // Types
    HEADER = 0,
    QUOTE = 1,
    LIST = 2,
    MEDIA = 3,
    HORIZONTAL = 4,
    CODE = 5,
    HTML = 6,
    PARAGRAPH = 7,
    EMPTY = 8,

    // Sub-Types,
    LIST_ITEM = 9,
    SUB_TEXT = 10,
    ALT_TEXT = 11,

    // Rich Text,
    BOLD = 12,
    ITALICS = 13,
    LINK = 14,
    INLINE_CODE = 15,
}