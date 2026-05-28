from types import SimpleNamespace
import os
import re
import json
import glob
from pathlib import Path
import argparse

# Types
HEADER = 0
QUOTE = 1
LIST = 2
MEDIA = 3
HORIZONTAL = 4
CODE = 5
HTML = 6
PARAGRAPH = 7
EMPTY = 8

# Sub-Types
LIST_ITEM = 9
SUB_TEXT = 10
ALT_TEXT = 11

# Rich Text
BOLD = 12
ITALICS = 13
LINK = 14
INLINE_CODE = 15

class MarkdownElement:
    def __init__(self, type, text="", data=""):
        self.type = type
        self.data = data
        self.text = text
        self.sub_elements = []

def convert(path):
    file_name = Path(path).stem
    print("\nConverting " + file_name + "...")

    print("Opening file...")
    lines_list = []
    with open(path, 'r') as file:
        for line in file:
            lines_list.append(line)

    print("Parsing File...")
    items = []
    for line in lines_list:
        # Header
        if re.search(r"^#{1,6}\s.+", line):
            items.append((MarkdownElement(HEADER), line))

        # Quote
        elif re.search(r"^>\s.+", line):
            items.append((MarkdownElement(QUOTE), line))

        # List
        elif re.search(r"^(\t| {2})*(-|[0-9]+\.)\s.+", line):
            items.append((MarkdownElement(LIST), line))

        # Media
        elif re.search(r"^!!?\[.+\]\(.+\..+\)$", line):
            items.append((MarkdownElement(MEDIA), line))

        # Horizontal
        elif re.search(r"^((---)|(___)|(\*\*\*))$", line):
            items.append((MarkdownElement(HORIZONTAL), line))

        # Code
        elif re.search(r"^```", line) or re.search(r"```$", line):
            items.append((MarkdownElement(CODE), line))

        # HTML
        elif re.search(r"^<>", line) or re.search(r"</>$", line):
            items.append((MarkdownElement(HTML), line))

        # Paragraph
        elif len(line.strip()) > 0:
            items.append((MarkdownElement(PARAGRAPH), line))

        # Empty
        else:
            items.append((MarkdownElement(EMPTY), None))
            
    print("Parsing Markdown...")
    parsed_elements = []
    current_element = None
    for item in items:
        element = item[0]
        line = item[1]

        # Continue List
        if current_element is not None and current_element.type == LIST:
            if element.type == LIST:
                li_element = MarkdownElement(LIST_ITEM)
                li_element.text = line.strip().split(' ', 1)[1].strip()
                
                degree = len(re.match(r'^(\t| {2})*', line).group()) // 2

                current_list = current_element
                for i in range(degree):
                    if len(current_element.sub_elements) > 0:
                        last_li = current_element.sub_elements[-1]

                        if len(last_li.sub_elements) == 0:
                            sub_list = MarkdownElement(LIST)
                            if re.search(r"^-\s", line.strip()):
                                sub_list.data = "unordered"
                            else: 
                                sub_list.data = "ordered"

                            last_li.sub_elements.append(sub_list)
                            current_list = sub_list
                        else:
                            current_list = last_li.sub_elements[-1]

                current_list.sub_elements.append(li_element)
                continue
        else:
            current_element = None

        # Continue Code
        if current_element is not None and current_element.type == CODE:
            if element.type == CODE and not re.search(r"^```", line):
                last_code_text = line[:-3].strip()
                if len(last_code_text) > 0:
                    code_element = MarkdownElement(SUB_TEXT)
                    code_element.text = last_code_text
                    current_element.sub_elements.append(code_element)
                current_element = None
                continue

            code_element = MarkdownElement(SUB_TEXT)
            code_element.text = line.strip()
            current_element.sub_elements.append(code_element)
            continue

        # Continue HTML
        if current_element is not None and current_element.type == HTML:
            if element.type == HTML and not re.search(r"^<>", line):
                last_code_text = line[:-3].strip()
                if len(last_code_text) > 0:
                    code_element = MarkdownElement(SUB_TEXT)
                    code_element.text = last_code_text
                    current_element.sub_elements.append(code_element)
                current_element = None
                continue

            code_element = MarkdownElement(SUB_TEXT)
            code_element.text = line.strip()
            current_element.sub_elements.append(code_element)
            continue

        # Create Header
        if element.type == HEADER:
            element.data = len(line.split(' ')[0]) - 1
            element.text = line.split(' ', 1)[1].strip()
            parsed_elements.append(element)
            continue

        # Create Quote
        if element.type == QUOTE:
            element.text = line.split(' ', 1)[1].strip()
            parsed_elements.append(element)
            continue
        
        # Start List
        if element.type == LIST:
            if re.search(r"^-\s", line):
                element.data = "unordered"
            else: 
                element.data = "ordered"

            li_element = MarkdownElement(LIST_ITEM)
            li_element.text = line.split(' ', 1)[1].strip()
            element.sub_elements.append(li_element)

            current_element = element
            parsed_elements.append(element)
            continue

        # Create Media
        if element.type == MEDIA:
            border = line.startswith("!!")
        
            split_index = line.rfind('](')
            alt = line[3:split_index] if border else line[2:split_index]
            file = line[(split_index + 2):len(line) - 1]

            element.data = border
            element.text = file
            
            alt_element = MarkdownElement(ALT_TEXT)
            alt_element.text = alt
            element.sub_elements.append(alt_element)

            parsed_elements.append(element)
            continue

        # Create Horizontal
        if element.type == HORIZONTAL:
            parsed_elements.append(element)
            continue

        # Create Code
        if element.type == CODE:
            element.data = line[2:].strip()
            current_element = element
            parsed_elements.append(element)
            continue

        # Create HTML
        if element.type == HTML:
            current_element = element
            parsed_elements.append(element)
            continue

        # Create Paragraph
        if element.type == PARAGRAPH:
            if current_element is not None and current_element.type == PARAGRAPH:
                current_element.text += (" " + line.strip())
            else:
                element.text = line.strip()
                current_element = element
                parsed_elements.append(element)
            continue

    print("Adding Rich Text...")

    rich_text_regex = re.compile(
        r'(?P<bold>\*\*(?P<bold_text>.+?)\*\*)'
        r'|(?P<italic>\*(?P<italic_text>.+?)\*)'
        r'|(?P<code>`(?P<code_text>.+?)`)'
        r'|(?P<link>\[(?P<link_text>.+?)\]\((?P<link_url>.+?)\))'
    )

    def parse_rich_text(text):
        tokens = []
        pos = 0

        for match in rich_text_regex.finditer(text):
            start, end = match.span()

            if start > pos:
                plain_text = MarkdownElement(SUB_TEXT)
                plain_text.text = text[pos:start]
                tokens.append(plain_text)

            if match.lastgroup == "bold":
                bold_text = MarkdownElement(BOLD)
                bold_text.text = match.group("bold_text")
                tokens.append(bold_text)

            elif match.lastgroup == "italic":
                ital_text = MarkdownElement(ITALICS)
                ital_text.text = match.group("italic_text")
                tokens.append(ital_text)

            elif match.lastgroup == "code":
                code_text = MarkdownElement(INLINE_CODE)
                code_text.text = match.group("code_text")
                tokens.append(code_text)

            elif match.lastgroup == "link":
                link_element = MarkdownElement(LINK)
                link_element.data = match.group("link_url")
                link_element.text = match.group("link_text")
                tokens.append(link_element)

            pos = end

        if pos < len(text):
            plain_text = MarkdownElement(SUB_TEXT)
            plain_text.text = text[pos:]
            tokens.append(plain_text)

        return tokens

    def parse_rich_list(list_items):
        for list_item in list_items:
            list_item.sub_elements = parse_rich_text(list_item.text) + list_item.sub_elements

            if len(list_item.sub_elements) > 0 and list_item.sub_elements[-1].type == LIST:
                parse_rich_list(list_item.sub_elements[-1].sub_elements)

    for element in parsed_elements:
        if element.type != PARAGRAPH and element.type != LIST:
            continue

        if element.type == PARAGRAPH:
            element.sub_elements = parse_rich_text(element.text)
            continue

        parse_rich_list(element.sub_elements)

    print("Saving Json...")
    data = SimpleNamespace()
    data.elements = parsed_elements

    os.makedirs("json", exist_ok=True)
    json_path = "json/" + file_name + ".json"
    dictionary = data.__dict__
    with open(json_path, "w") as json_file:
        json.dump(dictionary, json_file, default=vars)

parser = argparse.ArgumentParser()
group = parser.add_mutually_exclusive_group(required=True)
group.add_argument("name", nargs="?", help="A specific name")
group.add_argument("-a", "--all", action="store_true", help="All names")

args = parser.parse_args()

if args.all:
    files = glob.glob("markdown/*.md")
    for file in files:
        convert(file)
    print("\nConverted " + str(len(files)) + " Markdown Files.")
else:
    file = "markdown/" + args.name + ".md"
    convert(file)