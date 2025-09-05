# Task Manager API

## Project Description

This project is a backend service for managing personal tasks with user authentication. The system allows users to register, log in, and securely manage their tasks. Each user can create, update, delete, and query their own tasks. The application includes authentication and authorization using JWT, secure password storage, and filtering/pagination for efficient task management.

## Technologies Used

* **Backend:**

  * **ASP.NET Core 8** — framework for building the backend API.
  * **C#** — the primary programming language for implementing business logic and handling requests.
  * **Entity Framework Core** — ORM for database interaction.
  * **Database** — SQL Server, PostgreSQL, or MySQL.

* **Tools & Practices:**

  * **Architecture** — Clean Architecture with layered structure (API, Domain, Application, Persistence, Infrastructure, UI)
  * **Logging** — for tracking critical operations.
  * **Unit Testing** — for ensuring correctness of service and repository layers.

## Functionality

1. **User Registration**

   * Users can register with a username, email, and password.
   * Passwords are stored securely with hashing.

2. **Task Management**

   * **Create Task:** Add new tasks with title, description, due date, status, and priority.
   * **View Tasks:** Retrieve a list of tasks with filtering (by status, due date, priority) and pagination.
   * **View Task by ID:** Retrieve details of a specific task.
   * **Update Task:** Modify task details such as title, description, due date, status, and priority.
   * **Delete Task:** Remove tasks permanently.

## How to Run the Project Locally

### Getting Started

1. Clone the repository:

```bash
git clone https://github.com/Mkrager/Task-Manager.git
```

2. Open the project in your IDE (e.g., Visual Studio, Rider, VS Code).

3. Configure the database connection string in `appsettings.json`.

4. Apply database migrations:

```bash
dotnet ef database update
```

5. Run the application:

```bash
dotnet run
```

6. Access the API locally at:
   `http://localhost:5000`
