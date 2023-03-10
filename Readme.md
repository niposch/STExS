# What is STExS

STExS stands for Software Technology Examination System. This project is being developed by a group of students for the
Software Technology Module at TU Chemnitz.

This project is not production ready (and it is really not supposed to be used in production) as it is developed by
students as part of their course work.

- [Entwicklerhandbuch](./Entwicklerhandbuch.md)
- [Nutzerhandbuch](./benutzerhandbuch.md)
# How to run the project

## Recommended Dev Setup

- install Visual Studio 2022 or Jetbrains Rider
- install MS SQL Express 2019 => https://www.microsoft.com/de-de/sql-server/sql-server-downloads
- install the .NET 6 SDK => https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- https://nodejs.org/en/ (download the lts version)
- run in an elevated shell

```
npm install -g yarn @angular/cli ng-openapi-gen;
dotnet tool install --global dotnet-ef
```

- open the STExS.sln file in Visual Studio or Rider
- press start to start the backend
    - it might ask whether to install the dev certificate, press yes
- open a new terminal in the frontend folder
- run `yarn quickstart`
- you can inspect the backend api routes on https://localhost:44345/api/swagger
- the frontend will be started on http://localhost:4200

## Helpful commands

- https errors: run `dotnet dev-certs https` to enable the development https certificate (this only works on windows and
  mac)

## in visual studio code

- install the c# extension

## From Cli

### Backend

- go to the STExS.Web folder
- `dotnet run`

### Frontend

- start the backend
- after the backend started successfully run (from the frontend directory):

```powershell
yarn quickstart;
```

# How to use libraries

## Entity Framework

- the main commands one might use are: `database update` and `migrations add {new migration name}`
- do not run them directly (insert them into the placeholder below)
- be sure that you are in the backend subfolder of this repo when running these commands
- Run before running any command: `set ASPNETCORE_ENVIRONMENT=Development`
- for Application database:
    - `dotnet ef --project Repositories --startup-project STExS.Web {command} --context ApplicationDbContext`
- For reference: https://stackoverflow.com/a/60959348

## Asp.Net Identity

- provisional login page is on /Identity/Account/Login
# Architecture
## Grading
```mermaid
sequenceDiagram
participant user
participant exercise
participant grader

exercise --> exercise: state is new
user ->> exercise: requests details
activate user
exercise ->> user: delivers details
user -->> exercise: requests new time track to be created
exercise --> exercise: state is now "in progress"

user ->> user: starts working on exercise
user -->> exercise: submits temporary solutions + timetrack id
exercise -->> exercise: updates time track
exercise -->> exercise: updates temp submission
user -->> exercise: closes time track
deactivate user
user --> user: stops working on solution
activate user
user --> user: starts working on exercise again
user ->> exercise: queries last submitted temp solution
exercise->> user: returns last submitted temp solution
user -->> exercise: requests new time track to be created

user -->> exercise: submits temporary solutions + timetrack ids
user ->> exercise: submits final solution + timetrack id
exercise -->> exercise: closes time track
exercise --> exercise: state is now submitted
exercise -->> user: indicates success

deactivate user

exercise ->> grader: submits solutioon for grading
activate grader
grader ->> grader: grades solution
grader ->> exercise: publishes grading result
deactivate grader
exercise ->> exercise: state is now graded
user --> exercise: can always query their grading state
```
---
```mermaid
stateDiagram-v2
processing
notSubmitted
saveTemp
submitted
graded

[*] --> notSubmitted
[*] --> processing


processing --> submitted
processing --> saveTemp
saveTemp --> processing
submitted --> graded: happens automatically/manually

graded --> [*]
notSubmitted --> [*]

note left of processing: user works on a solution
```
---

```mermaid
classDiagram
direction TB

class Module{
	- 🔑 id: guid
}

class Chapter{
	- 🔑 id: guid
}
class Exercise{
	- 🔑 id: guid
}
class GradingResult{
	- 🔑 id: guid
	- userId: guid
	- exerciseId: Guid
	- CreationDate: DateTime
	- GradedSubmissionId: guid
}
class Submission{
	- 🔑 id: guid
	- 🔑 userSubmissionId: guid
	- ExerciseType: enum, discriminator
	- other data (per exercise type)
}
<<abstract>> Submission

class UserSubmission{
	- 🔑 UserId: guid
	- 🔑 ExerciseId: guid
	- FinalSubmission: guid?
}

class TimeTrack{
	- 🔑 id: Guid
	- StartDate: DateTime
	- ProcessingTime: TimeSpan 
}


Module "1" --> "m" Chapter
Chapter "1" --> "m" Exercise

Exercise "1" --> "0..1 per user" UserSubmission
UserSubmission "1" --> "0..m" Submission
UserSubmission "1" --> "m" TimeTrack
UserSubmission "1" --> "0..m" GradingResult
GradingResult "1" --> "0..1" Submission
```

- how the frontend works on a solution
- create timetrack
- work on solution
- save temp solution
- submit final solution
- close timetrack
```mermaid
sequenceDiagram
participant user
participant exercise

user ->> exercise: requests new time track to be created
exercise -->> exercise: creates time track
exercise ->> user: returns time track id
user ->> exercise: queries last submitted temp solution
exercise->> user: 404
activate user
user -->> user: starts working on solution new
user ->> exercise: saves temporary solutions + timetrack id
user ->> exercise: saves temporary solutions + timetrack id
user ->> exercise: saves temporary solutions + timetrack id
user ->> user: stops working on solution
deactivate user
user ->> exercise: closes time track
```
- diagram when temp solution exists
```mermaid
sequenceDiagram
participant user
participant exercise

user ->> exercise: requests new time track to be created
exercise -->> exercise: creates time track
exercise ->> user: returns time track id
user ->> exercise: queries last submitted temp solution

activate user
exercise->> user: returns last submitted temp solution
user -->> user: starts working on solution
user ->> exercise: saves temporary solutions + timetrack id
user ->> exercise: saves temporary solutions + timetrack id
user ->> exercise: submits final solution + timetrack id
deactivate user
```
- when the user submits a (temp) solution on a time track that is already closed a 403 is returned
- a time track is closed when the user submits a final solution
- time tracks can only be active for 48 hours

# Frontend

- start the backend
- after the backend started successfully run (from the frontend directory): `yarn quickstart`

- for Frontend documentation: `yarn docs`
- for Frontend tests: `yarn test` (chrome will open)
- for Frontend test-coverage: `yarn test-coverage` (see the generated .html file in /coverage/stex-s/)
