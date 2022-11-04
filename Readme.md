# What is STExS

STExS stands for Software Technology Examination System. This project is being developed by a group of students for the Software Technology Module at TU Chemnitz.

This project is not production ready (and it is really not supposed to be used in production) as it is developed by students as part of their course work.

Dokumentation Links:
[Einheitliche Terminologies](https://docs.google.com/spreadsheets/d/1g1vjrXWrB6KE0glshk8_LKinwjlBFHEBEaZ0eKljc3E)

# How to run the project

TODO

# How to use libraries

## Entity Framework

# EF Command

-   `set ASPNETCORE_ENVIRONMENT=Development`
    -   https://stackoverflow.com/a/60959348
-   `dotnet ef --project Repositories --startup-project STExS.Web`

-   update identity database (might be removed later)
-   `dotnet ef --project Repositories --startup-project STExS.Web migrations add --context AppIdentityDbContext AddedIdentity`
