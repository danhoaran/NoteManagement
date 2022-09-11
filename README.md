# NoteManagement

## What is this?
REST api built using .net 6 for carrying out basic CRUD operations. In addition, a simple web project for creating and displaying notes on a web page

## Getting started
You will need dotnet core 6.0 SDK to compile this

Api uses an in memory database so no need to run any migrations

To start, open NoteManagement.sln using your favourite IDE which supports .net 6. Visual Studio 2022 and JetBrains Rider 2021.3 onwards are compatible IDEs.

Find NoteManagementApi in the solultion explorer and start up a new debug instance (does not have to include debugging capabilities) 

You will find the swagger UI. Try out the GET api/Categories endpoint and you will see 3 entries added to the in-memory db via OnModelCreating overload in NoteManagementContext. Feel free to add more via the POST api/Categories endpoint.

Under Categories you will find all the endpoints for Notes. No existing data exists here so you will need to create a new note via the POST /api/Notes endpoint first. You are required to enter at least 1 valid category to create a note.

Go back to your IDE and find the web project NotesManagement.Web and start up a new instance in your favourite web browser. Please note that the existing NoteManagementApi project must be running for this project to work. After the page has loaded click Notes in the nav bar. You will find a form asking for categories and a body text area field. Create a note by entering text (this can be markdown) and selecting at least one category. After clicking submit the note will automatically be posted below the form under Notes

You will find a log file inside log folder in NoteManagementApi project root. Each time an api endpoint is accessed it will be logged in here.


