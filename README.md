\# FieldServiceTracker



\## Overview



FieldServiceTracker is a full-stack web application designed to help Field Services teams create, manage, track, and update service requests. The application demonstrates end-to-end data flow between a React frontend, ASP.NET Core Web API backend, and SQL Server database.



The project was developed using a layered architecture and follows modern software development practices including dependency injection, validation, logging, exception handling, RESTful APIs, and separation of concerns.



\---



\## Features



\### Service Request Management



\* Create new service requests

\* View all service requests

\* Update existing service requests

\* Delete service requests

\* Update request status independently using PATCH

\* Assign technicians to service requests



\### Search \& Filtering



\* Filter requests by Status

\* Filter requests by Priority

\* Search by:



&#x20; \* Ticket Number

&#x20; \* Customer Name

&#x20; \* Location

&#x20; \* Assigned Technician



\### Dashboard Summary



Displays real-time counts for:



\* Open Requests

\* In Progress Requests

\* Resolved Requests

\* Critical Priority Requests



\### User Experience Enhancements



\* Loading indicator while data is being fetched

\* Success messages after operations

\* Error handling with meaningful feedback

\* Empty-state messaging when no records match filters

\* Delete confirmation prompt



\---



\## Technology Stack



\### Backend



\* ASP.NET Core Web API (.NET 8)

\* C#

\* Entity Framework Core

\* SQL Server

\* FluentValidation

\* Serilog



\### Frontend



\* React

\* Vite

\* Axios

\* CSS



\### Tools



\* Visual Studio 2022

\* Visual Studio Code

\* Git

\* GitHub

\* Swagger / OpenAPI

\* SQL Server Management Studio (SSMS)



\---



\## Solution Structure



```text

FieldServiceTracker

│

├── FieldServiceTracker.sln

│

├── backend

│   └── FieldServiceTracker.API

│       ├── Controllers

│       ├── Data

│       ├── DTOs

│       ├── Exceptions

│       ├── Middleware

│       ├── Models

│       ├── Repositories

│       ├── Services

│       ├── Validators

│       └── Migrations

│

├── frontend

│   └── field-service-tracker-ui

│       ├── api

│       ├── components

│       └── assets

│

└── README.md

```



\---



\## Architecture



The application follows a layered architecture:



\### Controller Layer



Responsible for handling HTTP requests and returning responses.



\### Service Layer



Contains business logic and application rules.



\### Repository Layer



Responsible for database access and data persistence.



\### DTO Layer



Used to transfer data between API and client while avoiding direct exposure of database entities.



\### Validation Layer



Uses FluentValidation to validate incoming requests before business processing.



\### Middleware Layer



Provides centralized exception handling and consistent API error responses.



\---



\## Design Decisions



\### Dependency Injection



Dependency Injection is used throughout the application to improve maintainability, testability, and loose coupling between layers.



Examples:



\* IServiceRequestService

\* IServiceRequestRepository

\* AppDbContext



\---



\### Validation



FluentValidation is used to validate incoming requests.



Examples:



\* Customer Name is required

\* Location is required

\* Issue Description is required

\* Priority must be:



&#x20; \* Low

&#x20; \* Medium

&#x20; \* High

&#x20; \* Critical

\* Status must be:



&#x20; \* Open

&#x20; \* In Progress

&#x20; \* Resolved

&#x20; \* Closed



\---



\### Logging



Serilog is used to capture application events and errors.



Logs are written to:



```text

Logs/

```



Examples:



\* Service request created

\* Service request updated

\* Service request deleted

\* Unexpected exceptions



\---



\### Exception Handling



A global exception middleware is implemented to:



\* Catch unhandled exceptions

\* Log errors

\* Return consistent API responses



Examples:



```http

404 Not Found

500 Internal Server Error

```



\---



\### Duplicate Data Prevention



Ticket numbers are generated uniquely and a unique database index is configured to prevent duplicate service request creation.



\---



\### Timeout Handling



Axios is configured with a timeout to prevent the UI from waiting indefinitely when backend services become unavailable.



\---



\## REST API Endpoints



\### Get All Requests



```http

GET /api/ServiceRequests

```



Optional query parameters:



```http

GET /api/ServiceRequests?status=Open

GET /api/ServiceRequests?priority=High

```



\---



\### Get Request By Id



```http

GET /api/ServiceRequests/{id}

```



\---



\### Create Request



```http

POST /api/ServiceRequests

```



\---



\### Update Request



```http

PUT /api/ServiceRequests/{id}

```



\---



\### Update Status



```http

PATCH /api/ServiceRequests/{id}/status

```



\---



\### Delete Request



```http

DELETE /api/ServiceRequests/{id}

```



\---



\## HTTP Status Codes Used



| Status Code | Description           |

| ----------- | --------------------- |

| 200         | OK                    |

| 201         | Created               |

| 204         | No Content            |

| 400         | Bad Request           |

| 404         | Not Found             |

| 500         | Internal Server Error |



\---



\## Database Setup



Update the connection string in:



```text

appsettings.json

```



Example:



```json

{

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "Server=localhost;Database=FieldServiceTrackerDb;Trusted\_Connection=True;TrustServerCertificate=True"

&#x20; }

}

```



\### Run Migrations



Package Manager Console:



```powershell

Update-Database

```



\---



\## Running the Backend



Navigate to:



```text

backend/FieldServiceTracker.API

```



Run:



```bash

dotnet restore

dotnet run

```



Swagger UI:



```text

https://localhost:7098/swagger

```



\---



\## Running the Frontend



Navigate to:



```text

frontend/field-service-tracker-ui

```



Install dependencies:



```bash

npm install

```



Run:



```bash

npm run dev

```



Application:



```text

http://localhost:5173

```



\---



\## Future Enhancements



Potential improvements include:



\* Authentication and Authorization (JWT)

\* Role-Based Access Control

\* Pagination

\* Audit History

\* Email Notifications

\* Dashboard Analytics

\* Unit and Integration Testing

\* Docker Containerization

\* Cloud Deployment



\---



\## Notes



For this project, React was selected for the frontend due to recent hands-on development experience and the ability to deliver a production-ready solution within the project timeline. The backend remains aligned with Bell's preferred technology stack by utilizing ASP.NET Core, C#, SQL Server, Entity Framework Core, REST APIs, validation, logging, and dependency injection.



The focus of this implementation was to demonstrate clean architecture, maintainability, code quality, and practical software engineering principles while keeping the solution simple and easy to understand.



