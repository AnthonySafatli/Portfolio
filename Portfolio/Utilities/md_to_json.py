import md_to_enums 
import enums_to_json 
import json

file_name = input("File Name: ")
print("")

print("Reading in file")
md_file = "../Projects/Markdown/" + file_name + ".md"
lines_list = []
with open(md_file, 'r') as file:
    for line in file:
        lines_list.append(line.strip())

print("Parsing markdown")
data = []
for line in lines_list:
    enum = md_to_enums.get_enum(line)
    data.append((line, enum))
 
print("Generating json")
dictionary = enums_to_json.get_dict(data)

print("Saving json")
enums_to_json.save_dict(dictionary, "../Projects/Json/" + file_name + ".json")