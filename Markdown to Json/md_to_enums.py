import re

# Enums
HEADER_1 = 0
HEADER_2 = 1
HEADER_3 = 2
HEADER_4 = 3
HEADER_5 = 4
HEADER_6 = 5

QUOTE = 6

LIST = 7

MEDIA = 8

LINK = 9

HORIZONTAL = 10

CODE = 11

PARAGRAPH = 12
EMPTY = 13

# Convert md to enum
def get_enum(line):
    # Check if line is header
    header = re.search(r"^#{1,6}\s.+", line) 
    if header:
        return len(line.split()[0]) - 1
    
    # Check if line is quote
    quote = re.search(r"^>\s.+", line)
    if quote:
        return QUOTE
    
    # Check if line is list
    md_list = re.search(r"^\t*(-|[0-9]+\.)\s.+", line)
    if md_list:
        return LIST

    # Check if line is media element (photo or video)
    media = re.search(r"^!!?\[.+\]\(.+\..+\)$", line)
    if media:
        return MEDIA
    
    # Check if it is link
    link = re.search(r"^\[.+\]\(.+\)$", line)
    if link:
        return LINK
    
    # Check if line is horizontal line
    horizontal = re.search(r"^((---)|(___)|(\*\*\*))$", line)
    if horizontal:
        return HORIZONTAL

    # Check if line is code block
    code = re.search(r"^```", line)
    if code:
        return CODE

    # Check if its a paragraph or empty
    if len(line.strip()) > 0:
        return PARAGRAPH
    else:
        return EMPTY
    

