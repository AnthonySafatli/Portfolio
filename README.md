# Portfolio — My personal space to showcase my work online

![App Screenshot](https://anthonysafatli.ca/projects/screenshots/portfolio.png)

> A personal portfolio site built to impress on its own, not just display work. Features a fully custom frontend with an interactive 3D globe, a working backend, and a private admin panel with a built-in CRM.

🔗 **[anthonysafatli.ca](https://anthonysafatli.ca)**

## Features

- **Custom Frontend** — Built with Razor Pages and C#, with a Three.js-powered interactive globe as a centerpiece design element.
- **Admin Panel & CRM** — A private, login-protected admin section with full CRUD for managing projects, files, and contact messages.
- **Custom Markdown Converter** — Hand-built markdown parser that converts .md project write-ups into JSON, then renders them as HTML, no third-party library needed.

### Public Pages

- **Home** — Landing page with the Three.js globe
- **Projects** — Full list of projects pulled from the database
- **Project Pages** — Individual pages per project, authored in Markdown
- **About Me**
- **Contact** — Form that sends a direct email

### Admin Section (private)

- **Dashboard** — Overview of all projects and their metadata
- **Add / Edit Projects** — Create and update project entries in the database
- **File Manager** — Upload, browse, and delete media and files stored on the server

## Tech Stack
 
| Layer | Technology |
|-------|-----------|
| Frontend | Razor Pages, SCSS, Bootstrap, Three.js |
| Backend | ASP.NET, C#, Python |
| Auth | ASP.NET Identity Framework |
| Database | SQLite, Entity Framework ORM |

## What I Learned

- **Built a custom Markdown parser from scratch** — rather than using a library, wrote a pipeline that converts .md files to JSON to HTML. Forced a real understanding of parsing logic that a library would have hidden.
- **Deep dive into ASP.NET & Razor Pages** — learned the full request lifecycle, routing, and server-side rendering. Now one of my strongest skills.
- **Entity Framework ORM** — learned how C# models map to database tables and how to manage migrations. Made database work click for the first time.
- **ASP.NET Identity for auth** — implemented cookie-based authentication, password hashing, and route protection for the admin panel from scratch.
- **SCSS & Webpack** — learned how to structure stylesheets at scale and bundle assets; small exposure but a useful foundation.
- **Monolith-first lesson** — built everything coupled together; now understand why separating frontend/backend into a REST API + JS framework is worth the overhead. Would do it differently today.

## Roadmap

There are a lot of features I can add to this site

- [ ] A revamped frontend. Either with a frontend framework or just updated to be more modern
- [ ] More content sections
  - [ ] A blog section
  - [ ] A Game Jam Section
  - [ ] A Co-Op section
- [ ] Project tags and project search feature
- [ ] Upgraded admin experience
  - [ ] Updated and faster UI
  - [ ] Build-in markdown editor
  - [ ] Importing and exporting the projects DB and files
- [ ] A fake admin section admin sections for others to explore

## Licence

[GNU GPL v3](LICENCE)
