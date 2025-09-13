## Architecture Layers

### 1. Domain Layer

**Purpose:** Contains business entities, interfaces, and domain logic.

**Components:**

- **Models:**

  - `Employee` - Represents an employee
  - `MskRecommendation` - Represents an MSK recommendation
  - `EmployeeMskRecommendation` - Represents the assignment relationship
  - `BaseEntity` - Base class for all entities

- **Enums:**

  - `Type` - Recommendation types (TargetedExercise, WorkspaceAdjustment, BehavioralChange, LifestyleChange)
  - `Level` - Priority levels (Low, Medium, High)
  - `Status` - Assignment status (New, Assigned, InProgress, Completed, Rejected)

- **Interfaces:**
  - `IEmployeeRepository` - Employee data access contract
  - `IMskRecommendationRepository` - MSK recommendation data access contract

### 2. Application Layer

**Purpose:** Contains business logic, DTOs, and application services.

**Components:**

- **DTOs:**

  - `IEmployeeDto` / `EmployeeDto` - Employee data transfer objects
  - `IMskRecommendationDto` / `MskRecommendationDto` - MSK recommendation DTOs
  - `IEmployeeMskRecommendationDto` / `EmployeeMskRecommendationDto` - Assignment DTOs
  - `IKanbanBoardMskRecommendationDto` / `IKanbanBoardMskRecommendationDto` MSK recommendation DTOs for Kaban Board

- **Services:**

  - `IEmployeeService` / `EmployeeService` - Employee business logic
  - `IMskRecommendationService` / `MskRecommendationService` - MSK recommendation business logic

- **Mappings:**
  - `MappingProfile` - AutoMapper configuration

### 3. Infrastructure Layer

**Purpose:** Contains data access implementations and external dependencies.

**Components:**

- **Data:**

  - `MskManagementDbContext` - Entity Framework DbContext
  - `EFEmployeeRepository` - Employee repository implementation
  - `EFMskRecommendationRepository` - MSK recommendation repository implementation
  - `SeedData` - Initial data seeding
  - `SimpleDatabaseInitializer` - Database initialization service

- **Extensions:**
  - `ServiceCollectionExtensions` - Dependency injection configuration

### 4. API Layer

**Purpose:** Contains controllers, request/response DTOs, and API configuration.

**Components:**

- **Controllers:**

  - `EmployeesController` - Employee management endpoints
  - `MskRecommendationsController` - MSK recommendation management endpoints

- **DTOs:**

  - `AssignRecommendationRequest` - Request for assigning recommendations
  - `UpdateStatusRequest` - Request for updating status
  - `KanbanDataResponse` - Response for Kanban board data

- **Configuration:**
  - `Program.cs` - Application startup and configuration

## Database Schema

### Tables

1. **Employees**

   - Id (Guid, Primary Key)
   - FirstName (string)
   - LastName (string)
   - Email (string)
   - CreatedAt (DateTime)
   - UpdatedAt (DateTime)

2. **MskRecommendations**

   - Id (Guid, Primary Key)
   - Type (enum)
   - Level (enum)
   - Title (string)
   - Description (string)
   - CreatedAt (DateTime)
   - UpdatedAt (DateTime)

3. **EmployeeMskRecommendations**
   - Composite Primary Key: (EmployeeId, MskRecommendationId)
   - EmployeeId (Guid, Foreign Key)
   - MskRecommendationId (Guid, Foreign Key)
   - Status (enum)
   - CreatedAt (DateTime)
   - UpdatedAt (DateTime)
