# Portfolio

This is the repository for my Portfolio Website. It is being hosted on [anthonysafatli.ca](https://anthonysafatli.ca).

## Pages

### Public Pages

- Home Page
- Projects Page
  - This is a list of all my projects
- Individual Project Pages
  - This is a page unique to each project. This is written in markdown, then coverted to HTML
- About Me Page
- Contact Page
  - A page containing a form that will send me an email
### Admin Section
- Dashboard Page
  - A page with a list of my projects, showing information about each one
- Add Project Forms
  - A create and edit page for adding and editing projects to the DB
- File Manager
  - A page to view all the media and files saved on the server, as well as add and delete files

## Markdown Converter

This project also contains my own Markdown parser and converter. 

It is a python script that will take a simplified markdown file, and then convert it to a specific JSON format. This JSON is then saved in the servers database and will be used to render the project pages HTML content.

## Technology

This project uses an ASP.NET Web Server with an SQLite DB
