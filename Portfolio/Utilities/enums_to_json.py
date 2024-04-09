from ast import List
import md_to_enums as enum
import re
import json

TEXT = 0
TYPE = 1

END_CODE = enum.EMPTY + 1

def count_tabs(string):
    return len(string) - len(string.lstrip('\t'))

def get_dict(data):
    dictionary = { "elements" : [] }
    last_item = None

    for element in data:
        # Code type overrides everything
        code_override = False
        if element[TYPE] == enum.CODE and last_item == enum.CODE:
            last_item = END_CODE
            continue
        elif last_item == enum.CODE:
            dictionary["elements"][-1]["text"].append(element[TEXT])
            continue

        # Convert headers to dict
        if element[TYPE] <= enum.HEADER_6:
            degree = element[TYPE] + 1
            space_index = element[TEXT].find(' ')
            text = element[TEXT][space_index + 1:] # remove '#'s
            
            header = { "name": "header", "degree": degree, "text": text.strip() }
            dictionary["elements"].append(header)
            last_item = element[TYPE]
            continue

        if element[TYPE] == enum.QUOTE:
            text = element[TEXT][2:] # remove '>'
            
            quote = { "name": "quote", "text": text.strip() }
            dictionary["elements"].append(quote)
            last_item = element[TYPE]
            continue
        
        if element[TYPE] == enum.LIST:
            is_ordered = re.search(r"^\t*-\s.+", element[TEXT])
            if is_ordered:
                is_ordered = False
            else:
                is_ordered = True

            space_index = element[TEXT].strip().find(' ')
            list_item = element[TEXT].strip()[space_index+1:]
                
            if last_item == enum.LIST:
                if dictionary["elements"][-1]["ordered"] == is_ordered:
                    dictionary["elements"][-1]["items"].append(list_item)
                    continue

            items = [ list_item ]
            md_list = { "name": "list", "ordered": is_ordered, "items": items }
            dictionary["elements"].append(md_list)
            last_item = element[TYPE]
            continue 

        if element[TYPE] == enum.MEDIA:
            split_index = element[TEXT].rfind('](')
            alt = element[TEXT][2:split_index]
            file = element[TEXT][(split_index + 2):len(element[TEXT]) - 1]
            
            media = { "name": "media", "alt": alt, "file": file }
            dictionary["elements"].append(media)
            last_item = element[TYPE]
            continue

        if element[TYPE] == enum.LINK:
            split_index = element[TEXT].rfind('](')
            alt = element[TEXT][1:split_index]
            link = element[TEXT][(split_index + 2):len(element[TEXT]) - 1]
            
            link = { "name": "link", "alt": alt, "link": link }
            dictionary["elements"].append(link)
            last_item = element[TYPE]
            continue

        if element[TYPE] == enum.HORIZONTAL:
            horizontal = { "name": "horizontal" }
            dictionary["elements"].append(horizontal)
            last_item = element[TYPE]
            continue
        
        if element[TYPE] == enum.CODE:
            lang = element[TEXT][3:].strip()
            code = { "name": "code", "lang": lang, "text": [] }
            dictionary["elements"].append(code)
            last_item = element[TYPE]
            continue

        if element[TYPE] == enum.PARAGRAPH:
            text = element[TEXT].strip()

            if last_item == enum.PARAGRAPH:
                dictionary["elements"][-1]["text"] += " " + text
                continue

            paragraph = { "name": "paragraph", "text": text }
            dictionary["elements"].append(paragraph)
            last_item = element[TYPE]
            continue

        if element[TYPE] == enum.EMPTY:
            last_item = element[TYPE]
            continue
        
    return dictionary

def save_dict(dictionary, path):
    print(path)
    with open(path, "w") as json_file:
        json.dump(dictionary, json_file)