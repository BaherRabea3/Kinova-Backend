# Kinova API

![Build](https://img.shields.io/badge/build-passing-brightgreen)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0.30-informational)
![License](https://img.shields.io/badge/license-MIT-blue)
![Status](https://img.shields.io/badge/status-MVP-yellow)

---

## 1. Description & Architecture Overview

**Kinova** is a backend API for an AI-assisted physiotherapy platform. It supports two primary user roles — **Doctors** and **Patients** — and manages the full rehabilitation loop: doctors assign exercise **Plans** to patients, patients execute timed **Sessions** with per-repetition telemetry (joint angles and movement errors), and the system automatically calculates **Scores** and generates **Reports** for clinical review.

The solution follows **Clean Architecture** principles, separating concerns into four independent, dependency-inverted layers:

```
Kinova.Domain          → Enterprise-wide entities, enums, and business rules (no external dependencies)
Kinova.Application     → Use-cases via CQRS (MediatR), validation (FluentValidation), and abstractions/interfaces
Kinova.Infrastructure  → EF Core persistence, ASP.NET Core Identity, JWT issuance, external service implementations
Kinova.API             → ASP.NET Core Web API host: controllers, request/response DTOs, middleware, DI composition
```

**Key architectural patterns:**

- **CQRS with MediatR** — every use case is a discrete `Command` or `Query` handled by its own handler.
- **Result Pattern** — handlers return `Result<T>` instead of throwing exceptions for expected failures, mapped to appropriate HTTP status codes at the controller boundary.
- **Pipeline Behaviours** — cross-cutting concerns (logging, FluentValidation) are injected transparently into the MediatR pipeline.
- **Repository-free data access** — `IKinovaDbContext` is exposed directly to the Application layer as an abstraction over EF Core, avoiding redundant repository wrapping.
- **JWT Bearer authentication** with refresh-token rotation and role-based authorization (`Doctor`, `Patient`).

---

## 2. Key Features

- 🔐 JWT-based authentication with access + refresh token issuance and rotation
- 👥 Dual registration flows for **Doctors** and **Patients**, backed by ASP.NET Core Identity
- 📋 Doctor-authored **rehabilitation plans** with configurable sets, reps, and weekly frequency per exercise
- 🏋️ Session lifecycle management (`Start` → `Complete` / `Cancel`) with idempotent completion handling
- 📈 Automatic **scoring engine** (accuracy, range of motion, stability) computed from uploaded repetition telemetry
- 🦴 Per-repetition **joint angle** and **movement error** ingestion for motion-analysis pipelines
- 📄 Auto-generated clinical **reports** summarizing recurring movement errors per session
- 🔎 Doctor dashboard summary and searchable/paginated patient roster
- 🧩 Fluent, request-level validation via a dedicated MediatR pipeline behaviour
- 🌐 API versioning (`Asp.Versioning`) with Swagger/OpenAPI documentation out of the box

---

## 3. Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 8.0 |
| Web Framework | ASP.NET Core Web API |
| Data Access | Entity Framework Core 8 (SQL Server provider) |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| CQRS / Mediator | MediatR 14 |
| Validation | FluentValidation |
| API Versioning | Asp.Versioning.Mvc |
| API Documentation | Swashbuckle (Swagger / OpenAPI) |
| Database | Microsoft SQL Server |

---

## 4. Prerequisites

Before running the project locally, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or a full instance) — or a remote SQL Server connection string
- [EF Core CLI tools](https://learn.microsoft.com/ef/core/cli/dotnet) (project pins `dotnet-ef` via a local tool manifest)
- A REST client such as [Postman](https://www.postman.com/) or the built-in `Kinova.API.http` file (VS Code / Visual Studio)

---

## 5. Live Deployment

The API is deployed and publicly accessible on **Monster ASP**:

| Resource | URL |
|---|---|
| Base URL | [https://kinova.runasp.net/](https://kinova.runasp.net/) |
| Swagger UI | [https://kinova.runasp.net/swagger/index.html](https://kinova.runasp.net/swagger/index.html) |

Use the Swagger UI to explore and test all endpoints directly, including authenticating via the `Auth/login` endpoint and authorizing subsequent requests with the returned bearer token.

---

## 6. Local Installation, Database Migrations & Run Instructions

### 6.1 Clone & Restore

```bash
git clone https://github.com/<your-org>/kinova.git
cd kinova
dotnet restore
```

### 6.2 Configure the Database Connection

Update `Kinova.API/appsettings.Development.json` (or use `dotnet user-secrets`, see Configuration above) with a valid `DefaultConnection` string pointing at your SQL Server instance.

### 6.3 Install the EF Core CLI Tool (if not already available)

The repository pins the tool version via `Kinova.API/.config/dotnet-tools.json`:

```bash
dotnet tool restore
```

### 6.4 Apply Database Migrations

Run the following from the repository root (or from `Kinova.API` directly):

```bash
dotnet ef database update --project Kinova.Infrastructure --startup-project Kinova.API
```

This creates the `Kinova` database and applies all pending migrations, including seed data for roles, exercises, plans, and demo doctor/patient accounts.

### 6.5 Run the API

```bash
cd Kinova.API
dotnet run
```

The API will be available at:

- HTTP: `http://localhost:5116`
- HTTPS: `https://localhost:7141`
- Swagger UI: `https://localhost:7141/swagger`

### 6.6 (Optional) Create a New Migration After Model Changes

```bash
dotnet ef migrations add <MigrationName> --project Kinova.Infrastructure --startup-project Kinova.API
dotnet ef database update --project Kinova.Infrastructure --startup-project Kinova.API
```

---

## 7. API Reference

All endpoints are versioned and prefixed with `api/v{version}/[controller]` (e.g. `api/v1/Auth`). Endpoints marked 🔒 require a valid JWT bearer token; role-restricted endpoints are noted explicitly.

### Auth

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/v1/Auth/register/patient` | Register a new patient account and issue tokens |
| `POST` | `/api/v1/Auth/register/doctor` | Register a new doctor account and issue tokens |
| `POST` | `/api/v1/Auth/login` | Authenticate with email/password and receive a JWT + refresh token |
| `POST` | `/api/v1/Auth/logout` 🔒 | Invalidate the current user's refresh token |
| `POST` | `/api/v1/Auth/refresh` | Exchange an expired access token + valid refresh token for a new pair |

### Exercises

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/v1/Exercises` 🔒 | List exercises, filterable by search term, category, body part, difficulty |
| `GET` | `/api/v1/Exercises/{id}` 🔒 | Retrieve full details of a single exercise |

### Sessions

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/v1/Session/{PlanExercisItemId}` 🔒 (Patient) | Start a new session for an item in the patient's active plan |
| `POST` | `/api/v1/Session/{id}/complete` 🔒 (Patient) | **Telemetry ingestion endpoint** — submit recorded repetitions, joint angle readings, and movement errors to finalize a session |
| `POST` | `/api/v1/Session/{id}/cancel` 🔒 (Patient) | Cancel an in-progress session |

### Patient

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/v1/Patient/me` 🔒 (Patient) | Get the authenticated patient's profile details |
| `GET` | `/api/v1/Patient/me/plans/active` 🔒 (Patient) | List the patient's currently active rehabilitation plans |

### Doctor

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/v1/Doctor/profile` 🔒 (Doctor) | Get the authenticated doctor's profile |
| `PUT` | `/api/v1/Doctor/profile` 🔒 (Doctor) | Update doctor profile (name, license number, specialization) |
| `GET` | `/api/v1/Doctor/patients` 🔒 (Doctor) | Paginated, searchable list of the doctor's patients |
| `GET` | `/api/v1/Doctor/patients/{id}` 🔒 (Doctor) | Full detail view of a single patient, including plans and recent sessions |
| `GET` | `/api/v1/Doctor/patients/{id}/plans` 🔒 (Doctor) | List all plans assigned to a patient |
| `GET` | `/api/v1/Doctor/patients/{id}/reports` 🔒 (Doctor) | List all generated reports for a patient |
| `GET` | `/api/v1/Doctor/dashboard-summary` 🔒 (Doctor) | Aggregate dashboard metrics (active patients, sessions in progress, pending reports) |
| `POST` | `/api/v1/Doctor/plans` 🔒 (Doctor) | Create a new rehabilitation plan for a patient |
| `PUT` | `/api/v1/Doctor/plans/{id}` 🔒 (Doctor) | Update an existing plan |

#### Example — Telemetry Ingestion (Session Completion)

`POST /api/v1/Session/{id}/complete`

```json
{
  "reps": [
    {
      "repNumber": 1,
      "startTime": "2026-09-13T10:00:00Z",
      "endTime": "2026-09-13T10:00:04Z",
      "isCorrect": true,
      "jointAngleReadings": [
        { "jointName": "Knee", "angle": 92.5, "timestamp": "2026-09-13T10:00:02Z" }
      ],
      "movementErrors": [
        { "errorType": "Overextension", "bodyPart": "Knee", "severity": "Low", "deviationValue": 4.2 }
      ]
    }
  ]
}
```

**Response `200 OK`:**

```json
{
  "accuracyScore": 92.5,
  "rangeOfMotionScore": 78.3,
  "stabilityScore": 90.0,
  "overallScore": 86.9,
  "validRepetitions": 9,
  "invalidRepetitions": 1
}
```

---

## 8. Folder Structure

```
Kinova/
├── Kinova.sln
├── Kinova.API/                          # Presentation layer — hosts the Web API
│   ├── Controllers/                     # AuthController, DoctorController, PatientController, etc.
│   ├── Requests/                        # Incoming request DTOs, grouped by feature
│   ├── Exceptions/                      # GlobalExceptionHandler (IExceptionHandler)
│   ├── Properties/                      # launchSettings.json, publish profiles
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs                       # Composition root / app pipeline
│
├── Kinova.Application/                  # Application layer — use cases & orchestration
│   ├── Behaviours/                      # MediatR pipeline behaviours (Logging, Validation)
│   ├── Common/
│   │   ├── DTOs/                        # Response/transfer objects, grouped by feature
│   │   ├── Helpers/                     # ScoreCalculator, ReportContentBuilder
│   │   ├── Interfaces/                  # IKinovaDbContext, IJwtProvider, IAuthService, etc.
│   │   └── Settings/                    # Strongly-typed configuration (JwtOptions)
│   ├── Features/                        # CQRS commands/queries, grouped by domain feature
│   │   ├── Accounts/
│   │   ├── Doctors/
│   │   ├── Exercises/
│   │   ├── Patients/
│   │   └── Sessions/
│   └── DependencyInjection.cs
│
├── Kinova.Domain/                       # Domain layer — entities & core business rules
│   ├── Common/                          # Result, Error, ValidationResult
│   └── Entities/                        # Doctor, Patient, Exercise, Plan, Session, Score, Report, ...
│
├── Kinova.Infrastructure/                # Infrastructure layer — persistence & external services
│   ├── Identity/                        # ApplicationUser, ApplicationRole
│   ├── Persistence/
│   │   ├── Configurations/              # IEntityTypeConfiguration<T> per entity
│   │   ├── Migrations/                  # EF Core migrations
│   │   ├── KinovaDbContext.cs
│   │   └── DbSeeder.cs
│   ├── Services/AuthServices/           # AuthService, JwtProvider
│   └── DependencyInjection.cs
│
└── .gitignore
```

---

## 9. License

This project is licensed under the **MIT License**. See the `LICENSE` file for details.
