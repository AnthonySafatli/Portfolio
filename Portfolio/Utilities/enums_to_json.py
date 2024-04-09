import md_to_enums as enum
import json

TEXT = 0
TYPE = 1

def get_dict(data):
    dictionary = { "elements" : [] }

    for element in data:
        # Convert headers to dict
        if element[TYPE] <= enum.HEADER_6:
            degree = element[TYPE] + 1
            space_index = element[TEXT].find(' ')
            text = element[TEXT][space_index + 1:] # remove '#'s
            
            header = { "name": "header", "degree": degree, "text": text.strip() }
            dictionary["elements"].append(header)
            continue

        if element[TYPE] == enum.QUOTE:
            text = element[TEXT][2:] # remove '>'
            
            quote = { "name": "quote", "text": text.strip() }
            dictionary["elements"].append(quote)
            continue
        
        if element[TYPE] == enum.ORDERED_LIST or element[TYPE] == enum.LIST:
            continue # remember to manage nested lists and tabs

        if element[TYPE] == enum.MEDIA:
            split_index = element[TEXT].rfind(')[')
            file = element[TEXT][2:split_index]
            alt = element[TEXT][(split_index + 2):len(element[TEXT])]
            
            media = { "name": "media", "file": file, "alt": alt }
            dictionary["elements"].append(media)
            continue

        if element[TYPE] == enum.LINK:
            link = None
            alt = None
            
            link = { "name": "link", "link": link, "alt": alt }
            dictionary["elements"].append(link)
            continue

        if element[TYPE] == enum.HORIZONTAL:
            horizontal = { "name": "horizontal" }
            dictionary["elements"].append(horizontal)
            continue

        if element[TYPE] == enum.PARAGRAPH:
            text = element[TEXT].strip()

            paragraph = { "name": "paragraph", "text": text }
            dictionary["elements"].append(paragraph)
            continue

        if element[TYPE] == enum.EMPTY:
            continue

            
        
    return dictionary

def save_dict(dictionary, path):
    pass