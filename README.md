# MSK Suggestion Management API

This is the backend API I built for managing MSK (Musculoskeletal) recommendations and employee assignments. It's a .NET 9 Web API with Clean Architecture that serves data to the frontend Kanban board.

## Getting Started

### What You Need

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Entity Framework Core Tools (if you want to mess with migrations)

### How to Run It

1. **Clone the repo**
   git clone https://github.com/ahmedkhalifa367/MskSuggestionManagement
2. **Restore packages**
   dotnet restore
3. **Run the app**
   dotnet run
4. **Check it out**
   - API: `https://localhost:7230/api`
   - Swagger docs: `https://localhost:7230/swagger`

## API Endpoints

### Employee Stuff

- `GET /api/employees` - Get all employees
- `POST /api/employees` - Add a new employee

### MSK Recommendations

- `GET /api/mskrecommendations` - Get all recommendations for the Kanban board
- `GET /api/mskrecommendations/vida` - Get recommendations from Vida system
- `GET /api/mskrecommendations/assignments` - Get all assignments
- `POST /api/mskrecommendations/assign` - Assign a recommendation to someone
- `PUT /api/mskrecommendations/status` - Update assignment status
- `PUT /api/mskrecommendations/reassign` - Reassign to a different employee

## Database Schema

### What I'm Using Now

I'm using Entity Framework Core with an in-memory database for the demo. It's not production-ready, but it works for showing how the system would work.

### How I'd Structure a Real Database

```sql
-- Employees table
CREATE TABLE Employees (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Department NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);

-- MSK Recommendations table
CREATE TABLE MskRecommendations (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Type INT NOT NULL, -- Enum: TargetedExercise, WorkspaceAdjustment, etc.
    Level INT NOT NULL, -- Enum: Low, Medium, High
    Description NVARCHAR(MAX) NOT NULL,
    Source NVARCHAR(50) NOT NULL, -- 'Employee' or 'Vida'
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);

-- Assignment relationship (Many-to-Many)
CREATE TABLE EmployeeMskRecommendations (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmployeeId UNIQUEIDENTIFIER NOT NULL,
    MskRecommendationId UNIQUEIDENTIFIER NOT NULL,
    Status INT NOT NULL, -- Enum: New, Assigned, InProgress, Completed, Rejected
    AssignedAt DATETIME2 DEFAULT GETUTCDATE(),
    CompletedAt DATETIME2 NULL,
    Notes NVARCHAR(MAX) NULL,

    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE,
    FOREIGN KEY (MskRecommendationId) REFERENCES MskRecommendations(Id) ON DELETE CASCADE,
    UNIQUE(EmployeeId, MskRecommendationId)
);
```

### Indexes for Performance

```sql
-- Performance indexes
CREATE INDEX IX_EmployeeMskRecommendations_EmployeeId ON EmployeeMskRecommendations(EmployeeId);
CREATE INDEX IX_EmployeeMskRecommendations_Status ON EmployeeMskRecommendations(Status);
CREATE INDEX IX_EmployeeMskRecommendations_AssignedAt ON EmployeeMskRecommendations(AssignedAt);
CREATE INDEX IX_MskRecommendations_Source ON MskRecommendations(Source);
CREATE INDEX IX_Employees_Email ON Employees(Email);
```

I went with **Clean Architecture** because:

- **Domain Layer** - Core business logic and entities
- **Application Layer** - Use cases and business rules
- **Infrastructure Layer** - Data access and external stuff
- **API Layer** - Controllers and presentation

I like this approach because:

- Each layer has a clear responsibility
- Easy to test each layer independently
- Can swap out implementations (like changing databases)
- Follows SOLID principles

### Technology Choices

- **.NET 9** - Latest and greatest, good performance
- **Entity Framework Core** - ORM that actually works well
- **AutoMapper** - Maps between DTOs and entities without writing boring mapping code
- **Swagger** - Auto-generates API documentation

## What I Assumed

1. **Data Volume** - Hundreds of employees, thousands of recommendations (not millions)
2. **User Roles** - One admin managing everyone (no multi-tenant complexity)
3. **Integration** - Vida system sends data via REST API (I simulated this)
4. **Real-time** - No need for real-time collaboration
5. **Security** - Development mode without auth (would add JWT in production)
6. **Performance** - In-memory database is fine for demo (would use SQL Server in production)

## What I'd Do With More Time

### Backend Improvements

- **Authentication & Authorization** - JWT tokens with proper roles
- **Security** - Development mode without auth (would add JWT in production)
- **User Roles** - One admin managing everyone (no multi-tenant complexity)
- **Real Database** - SQL Server or PostgreSQL with proper migrations
- **Caching** - Redis for frequently accessed data
- **Logging** - Serilog with correlation IDs for debugging
- **Unit/Tests** - Actually test my code (I know, I should have)
- **Input Validation** - FluentValidation for comprehensive validation
