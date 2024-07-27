import md_to_enums 
import enums_to_json 
import json
import sys

file_name = sys.argv[1]

md_file = "Projects/Markdown/" + file_name + ".md"


lines_list = []
with open(md_file, 'r') as file:
    for line in file:
        lines_list.append(line.strip())

data = []
for line in lines_list:
    enum = md_to_enums.get_enum(line)
    data.append((line, enum))
 
dictionary = enums_to_json.get_dict(data)

dict_json = json.dumps(dictionary)
print(dict_json)