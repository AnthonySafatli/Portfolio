## Overview

This online portfolio was built to showcase my software developement skills and projects in an accessable and interactive way for potential employers, collaberators and fellow developers. Developed using ASP.NET for both the backend and frontend, with a SQLite database to manage project data, this platform serves as a dynamic and flexible space for hosting content. The most notable port of this project is the hidden away admin pages, which allow me to manage all project related content and media. 

### Technologies

- Frontend: The frontend uses Razor Pages, CSS/SCSS and JS. Three.JS was also used to generate anique and impressive looking graphics
- Backend: The backend uses ASP.NET and C# with a custom built admin section
- Database: SQLite is used with Entity Framwork to store information about projects
- Other: Python scripts are used to convert Markdown files into JSON, which is used to display project pages (such as this)

## Features

### Admin Section

My custom-built admin section of this portfolio website is the most sophisticated part of the project. It features multiple pages, including a main dashboard for managing projects and a dedicated dashboard for handling various files �such as images, videos, and Markdown files� used across the site. The admin interface also includes specialized pages for uploading and organizing media, offering a streamlined experience for maintaining and updating content efficiently.

![Dashboard Screenshot](/projects/images/admin-dashboard.jpg)

The main dashboard is used for managing the projects in the database. It shows the general information for each one in a nice table view. You can then view each one in more detail on another page. That same page also lets you modify and delete each project entry

![File Dashboard Screenshot](/projects/images/admin-file-dashboard.jpg)

The file manager dashboard provides a comprehensive overview of all media files used across the website, including screenshots, videos, and logos. It allows you to see which files are in use and view all Markdown files for project pages. If files are updated, you can run a Python script to refresh the JSON data. The dashboard also displays the status of both JSON files and media, streamlining updates without needing to access individual project pages.

![Upload Screenshot](/projects/images/admin-upload-dashboard.jpg)

The admin section also lets you upload anything you would need to seemlessly. This includes adding a project to the database, uploading/updating a markdown script for a project and uploading images/videos.

### Versatile Hosting for Projects

The portfolio website is designed to serve not only as a showcase of my work but also as a flexible platform for hosting smaller web projects. Instead of building a separate website for each idea, I can easily create new pages within this portfolio to display and experiment with various concepts. This approach allows for rapid development, seamless integration, and consistent styling across all projects, providing a unified experience for visitors.

#### Examples

Here are a few examples:

[An interactive data visualization tool using Three.js](https://randomlink.com)
[A simple web-based game](https://whatever.ca)
[A demo page for a custom UI component or design system](https://a.ca)