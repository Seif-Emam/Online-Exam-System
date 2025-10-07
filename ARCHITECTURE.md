# Architecture Overview

**Project:** Online-Exam-System  
**Pattern:** Clean Architecture with CQRS  
**Tech Stack:** ASP.NET Core 8.0, EF Core 8.0, MediatR, SQL Server

---

## 📐 High-Level Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                         API Layer                                │
│  (Controllers - HTTP Endpoints)                                  │
│  - AuthController                                                │
│  - DiplomaController                                             │
│  - ExamController                                                │
│  - QuestionController                                            │
└────────────────────────┬────────────────────────────────────────┘
                         │
                         │ HTTP Request
                         ▼
┌─────────────────────────────────────────────────────────────────┐
│                    Middleware Pipeline                           │
│  1. GlobalExceptionMiddleware (Error Handling)                   │
│  2. Authentication (JWT Bearer)                                  │
│  3. Authorization (Role-based)                                   │
└────────────────────────┬────────────────────────────────────────┘
                         │
                         │ Validated Request
                         ▼
┌─────────────────────────────────────────────────────────────────┐
│              Application Layer (Features)                        │
│  CQRS Pattern with MediatR                                       │
│                                                                   │
│  ┌───────────────┐  ┌───────────────┐  ┌───────────────┐       │
│  │   Commands    │  │    Queries    │  │  Orchestrators│       │
│  │  (Write Ops)  │  │  (Read Ops)   │  │  (Complex)    │       │
│  └───────┬───────┘  └───────┬───────┘  └───────┬───────┘       │
│          │                  │                   │                │
│          ▼                  ▼                   ▼                │
│  ┌───────────────────────────────────────────────────┐          │
│  │         FluentValidation Pipeline                  │          │
│  │  (Validates commands/queries)                      │          │
│  └───────────────────┬───────────────────────────────┘          │
│                      │                                           │
│                      ▼                                           │
│  ┌───────────────────────────────────────────────────┐          │
│  │              Handlers                              │          │
│  │  - RegisterHandler                                 │          │
│  │  - LoginHandler                                    │          │
│  │  - GetAllDiplomasHandler                           │          │
│  │  - GetExamByIdHandler                              │          │
│  │  - etc.                                            │          │
│  └───────────────────┬───────────────────────────────┘          │
└──────────────────────┼──────────────────────────────────────────┘
                       │
                       │ Business Logic
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                            │
│                                                                   │
│  ┌──────────────────────┐      ┌──────────────────────┐         │
│  │   Repositories        │      │      Services        │         │
│  │  - UnitOfWork         │      │  - JwtService        │         │
│  │  - GenericRepository  │      │  - ImageHelper       │         │
│  └──────────┬────────────┘      └──────────┬───────────┘         │
│             │                              │                     │
│             ▼                              ▼                     │
│  ┌──────────────────────┐      ┌──────────────────────┐         │
│  │   EF Core Context    │      │   File System        │         │
│  │  OnlineExamContext   │      │   (wwwroot/Uploads)  │         │
│  └──────────┬────────────┘      └──────────────────────┘         │
└─────────────┼──────────────────────────────────────────────────┘
              │
              │ SQL Queries
              ▼
┌─────────────────────────────────────────────────────────────────┐
│                     Database Layer                               │
│                   SQL Server Database                            │
│                                                                   │
│  Tables:                                                         │
│  - Users (Identity)                                              │
│  - Roles (Identity)                                              │
│  - Diplomas                                                      │
│  - Exams                                                         │
│  - Questions                                                     │
│  - Choices                                                       │
│  - Answers                                                       │
└─────────────────────────────────────────────────────────────────┘
```

---

## 🔄 Request Flow Example: User Login

```
1. POST /api/auth/login
   │
   ├─> [GlobalExceptionMiddleware]
   │
   ├─> [AuthController.Login(LoginCommand)]
   │
   ├─> [MediatR] Sends LoginCommand
   │   │
   │   ├─> [LoginValidator] (FluentValidation)
   │   │   - Validates email format
   │   │   - Validates password requirements
   │   │
   │   └─> [LoginHandler]
   │       │
   │       ├─> UserManager.FindByEmailAsync()
   │       │
   │       ├─> UserManager.CheckPasswordAsync()
   │       │
   │       ├─> UserManager.GetRolesAsync()
   │       │
   │       └─> JwtService.GenerateToken()
   │           │
   │           └─> Returns JWT Token
   │
   └─> Returns LoginResponse with JWT
```

---

## 🔄 Request Flow Example: Add Diploma (Admin Only)

```
1. POST /api/diploma
   │
   ├─> [GlobalExceptionMiddleware]
   │
   ├─> [Authentication Middleware]
   │   - Validates JWT Token
   │   - Populates User.Claims
   │
   ├─> [Authorization Middleware]
   │   - Checks [Authorize(Policy = "AdminOnly")]
   │   - Verifies User.IsInRole("Admin")
   │
   ├─> [DiplomaController.AddDiploma()]
   │
   ├─> [AddDiplomaOrchestrator] (Complex flow with file upload)
   │   │
   │   ├─> ImageHelper.SaveImageAsync()
   │   │   - Validates file
   │   │   - Saves to wwwroot/Uploads/Images/
   │   │   - Returns relative path
   │   │
   │   ├─> [MediatR] Sends AddDiplomaCommand
   │   │   │
   │   │   └─> [AddDiplomaHandler]
   │   │       │
   │   │       ├─> UnitOfWork.GetRepository<Diploma>()
   │   │       │
   │   │       ├─> Repository.AddAsync(diploma)
   │   │       │
   │   │       └─> UnitOfWork.SaveChangesAsync()
   │   │
   │   └─> ImageHelper.GetImageUrl()
   │       - Converts relative path to full URL
   │
   └─> Returns AddDiplomaDTO with image URL
```

---

## 📦 Layer Dependencies

```
┌─────────────────────────────────────────────┐
│           Presentation Layer                 │
│  (Controllers, DTOs, Requests/Responses)     │
│  Depends on: Application Layer               │
└────────────────┬────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────┐
│          Application Layer                   │
│  (Features, Handlers, Commands, Queries)     │
│  Depends on: Domain Layer, Contracts         │
└────────────────┬────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────┐
│            Domain Layer                      │
│  (Models/Entities, Business Rules)           │
│  Depends on: Nothing (Pure domain)           │
└────────────────┬────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────┐
│        Infrastructure Layer                  │
│  (Repositories, Services, EF Context)        │
│  Depends on: Domain Layer, Contracts         │
└─────────────────────────────────────────────┘

Note: Actual implementation has some violations
- Program.cs references all layers (composition root)
- Some circular dependencies exist
- Contracts folder acts as interfaces layer
```

---

## 🗂️ Folder Structure

```
Online-Exam-System/
├── Contarcts/              # Interfaces (abstraction layer)
│   ├── IUnitOfWork.cs
│   ├── IGenericRepository.cs
│   ├── ITokenService.cs
│   ├── IImageHelper.cs
│   └── I*Orchestrator.cs
│
├── Data/                   # Data access layer
│   ├── OnlineExamContext.cs
│   ├── Configurations/     # EF Core entity configs
│   ├── Migrations/         # EF migrations
│   └── seed/               # Database seeders
│
├── Features/               # Feature-based organization (CQRS)
│   ├── Auth/
│   │   ├── Login/
│   │   │   ├── LoginCommand.cs
│   │   │   ├── LoginHandler.cs
│   │   │   ├── LoginValidator.cs
│   │   │   └── LoginResponse.cs
│   │   ├── Register/
│   │   ├── ChangePassword/
│   │   ├── GetCurrentUser/
│   │   └── UpdateUserProfile/
│   │
│   ├── Diploma/
│   │   ├── AddDiploma/
│   │   ├── UpdateDiploma/
│   │   ├── DeleteDiploma/
│   │   ├── GetAllDiplomas/
│   │   └── GetDiplomaById/
│   │
│   ├── Exam/
│   └── Question/
│
├── Middlewares/            # Custom middleware
│   └── GlobalExceptionMiddleware.cs
│
├── Models/                 # Domain entities
│   ├── ApplicationUser.cs
│   ├── Diploma.cs
│   ├── Exam.cs
│   ├── BaseEntity.cs
│   └── Questions/
│
├── Repositories/           # Data access implementations
│   ├── UnitOfWork.cs
│   ├── GenericRepository.cs
│   └── JwtService.cs
│
├── Services/               # Application services
│   └── ImageHelper.cs
│
├── Shared/                 # Shared utilities
│   └── ValidationBehavior.cs
│
├── wwwroot/                # Static files
│   └── Uploads/
│       └── Images/
│
├── Program.cs              # Application entry point
├── appsettings.json        # Configuration
└── Online-Exam-System.csproj
```

---

## 🔌 Key Design Patterns

### 1. **CQRS (Command Query Responsibility Segregation)**
- **Commands:** Modify state (Create, Update, Delete)
- **Queries:** Read state (Get, GetAll, GetById)
- Implemented via MediatR library
- Handlers separated by responsibility

### 2. **Repository Pattern**
- Abstracts data access
- Generic repository for common CRUD operations
- Specific repositories can be added for complex queries

### 3. **Unit of Work Pattern**
- Manages transactions across multiple repositories
- Single SaveChanges() for all operations
- Ensures data consistency

### 4. **Orchestrator Pattern**
- Used for complex multi-step operations
- Examples: AddDiplomaOrchestrator, UpdateExamOrchestrator
- Coordinates file operations + database operations

### 5. **Mediator Pattern**
- Decouples request handling from controllers
- Handlers are independent and testable
- Pipeline behaviors for cross-cutting concerns (validation)

### 6. **Middleware Pipeline**
- Request/response processing chain
- Exception handling, authentication, authorization
- Extensible and modular

---

## 🔐 Authentication & Authorization Flow

```
┌─────────────────────────────────────────────────────────────┐
│  1. User Registers                                          │
│     POST /api/auth/register                                 │
│     - Validate input (FluentValidation)                     │
│     - Create user in Identity system                        │
│     - Assign "User" role by default                         │
│     - Return success response                               │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│  2. User Logs In                                            │
│     POST /api/auth/login                                    │
│     - Validate credentials (UserManager)                    │
│     - Get user roles (UserManager.GetRolesAsync)            │
│     - Generate JWT with claims:                             │
│       • id: user.Id                                         │
│       • email: user.Email                                   │
│       • roles: ["User"] or ["Admin"]                        │
│       • jti: unique token ID                                │
│     - Return JWT token + user info                          │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│  3. User Accesses Protected Endpoint                        │
│     GET /api/diploma  [Authorize]                           │
│     - Extract JWT from Authorization header                 │
│     - Validate token (signature, expiration, issuer)        │
│     - Populate User.Claims from token                       │
│     - Allow access if valid                                 │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│  4. Admin Accesses Admin-Only Endpoint                      │
│     POST /api/diploma  [Authorize(Policy = "AdminOnly")]    │
│     - Validate token (as above)                             │
│     - Check User.IsInRole("Admin")                          │
│     - Allow access if admin, else 403 Forbidden             │
└─────────────────────────────────────────────────────────────┘
```

---

## 🔒 Security Layers

```
Layer 1: HTTPS
  └─> Encrypts data in transit

Layer 2: JWT Authentication
  └─> Verifies user identity

Layer 3: Authorization Policies
  └─> Enforces role-based access

Layer 4: Input Validation
  └─> FluentValidation + Model validation

Layer 5: Exception Handling
  └─> GlobalExceptionMiddleware (prevents info leakage)

Layer 6: Database Security
  └─> Parameterized queries (EF Core)
```

---

## 📊 Data Flow

### Write Operation (Command)
```
Controller → Orchestrator (if complex) → MediatR → Validator → Handler → Repository → UnitOfWork → DbContext → Database
```

### Read Operation (Query)
```
Controller → MediatR → Handler → Repository → DbContext → Database
```

### File Upload (Complex)
```
Controller → Orchestrator → ImageHelper (save file) → MediatR → Handler → Repository → UnitOfWork → Database
                                                    ↓
                                              wwwroot/Uploads/
```

---

## 🚀 Scaling Considerations

### Current Limitations:
1. **File Storage:** Local file system (wwwroot) - not scalable
2. **Database:** Single SQL Server instance
3. **Caching:** In-memory only (not shared across instances)
4. **Sessions:** JWT is stateless (good for scaling)

### Scaling Path:
1. Move images to cloud storage (Azure Blob/S3)
2. Add Redis for distributed caching
3. Use database read replicas for read-heavy operations
4. Add CDN for static content and images
5. Implement API Gateway for load balancing
6. Use message queue for async operations (if needed)

---

## 🎯 Strengths of Current Architecture

✅ Clean separation of concerns  
✅ Testable (MediatR handlers are isolated)  
✅ Scalable pattern (CQRS allows read/write optimization)  
✅ Feature-based organization (easy to find related code)  
✅ Extensible (pipeline behaviors, middleware)  
✅ Modern async/await patterns  

---

## ⚠️ Architectural Concerns

❌ Inconsistent orchestrator usage  
❌ No clear layering (some dependencies cross layers)  
❌ File storage not production-ready  
❌ No caching strategy defined  
❌ No API versioning  
❌ Database seeding runs on every startup  

---

## 📚 References

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR/wiki)
- [ASP.NET Core Architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/)

---

*This document provides a high-level overview. See CODE_REVIEW.md for detailed analysis.*
