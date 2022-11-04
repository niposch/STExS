# What is STExS

STExS stands for Software Technology Examination System. This project is being developed by a group of students for the Software Technology Module at TU Chemnitz.

This project is not production ready (and it is really not supposed to be used in production) as it is developed by students as part of their course work.

Dokumentation Links:
[Einheitliche Terminologies](https://docs.google.com/spreadsheets/d/1g1vjrXWrB6KE0glshk8_LKinwjlBFHEBEaZ0eKljc3E)

# How to run the project

-   dependencies
    -   (download and install the sdk) https://dotnet.microsoft.com/en-us/download/dotnet/6.0
    -   (download lts) https://nodejs.org/en/
    -   (run in an elevated shell) `npm install -g yarn`
    -   (run in an elevated shell) `npm install -g @angular/cli`
-   open the project in visual studio or rider and run the backend through the launch options (the reccomended way of starting the project)
-   you can inspect the backend routes on https://localhost:44345/swagger

## First Start

-   run `dotnet dev-certs https` to enable the development https certificate (this only works on windows and mac)

## From Cli

### Backend

-   go to the STExS.Web folder
-   `dotnet run`

### Frontend

-   start the backend
-   after the backend started successfully run:

```powershell
yarn;
yarn quickstart;
```

# How to use libraries

## Entity Framework

-   the main commands one might use are: `database update` and `migration add {new migration name}`
-   do not run them directly (insert them into the placeholder for the identity database or the application database)
-   be sure that you are in the backend subfolder of this repo when running these commands
-   Run before running any command: `set ASPNETCORE_ENVIRONMENT=Development`
-   for Application database:
    -   `dotnet ef --project Repositories --startup-project STExS.Web {command}`
-   for Identity database(might be removed later):
    -   `dotnet ef --project Identity --startup-project STExS.Web {command}`
-   For reference: https://stackoverflow.com/a/60959348
