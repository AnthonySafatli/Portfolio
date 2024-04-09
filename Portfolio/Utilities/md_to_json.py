import md_to_enums 
import enums_to_json 
import json

file = input("File Name: ")

def read_file(filename):
    lines_list = []
    with open(filename, 'r') as file:
        for line in file:
            lines_list.append(line.strip())
    return lines_list

md_file = '../Projects/Markdown/' + file + ".md"
lines_list = read_file(md_file)

data = []
for line in lines_list:
    enum = md_to_enums.get_enum(line)
    data.append((line, enum))
    
dictionary = enums_to_json.get_dict(data)

enums_to_json.save_dict(dictionary, "../Projects/Json/" + file + ".json")