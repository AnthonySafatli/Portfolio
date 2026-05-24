import md_to_enums 
import enums_to_json 
import json
import sys

file_name = input("File Name: ")

md_file = "markdown/" + file_name + ".md"

print("Opening File...")
lines_list = []
with open(md_file, 'r') as file:
    for line in file:
        lines_list.append(line.strip())

print("Converting to enums...")
data = []
for line in lines_list:
    enum = md_to_enums.get_enum(line)
    data.append((line, enum))
 
print("Converting to JSON...")
dictionary = enums_to_json.get_dict(data)

print("Saving to file...")
enums_to_json.save_dict(dictionary, "json/" + file_name + ".json")