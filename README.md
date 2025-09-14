# MSK Suggestion Management System

This is a full-stack application I built to help manage MSK (Musculoskeletal) recommendations for employees. It's basically a Kanban board where you can assign health recommendations to people and track their progress.

The idea came from a regional director who needed to reduce MSK-related absences in their company. They're using VIDA to generate recommendations, but they needed a way to actually track if employees are following through with them.

## What This Does

- **Backend API** (.NET 9) - Handles all the data and business logic
- **Frontend** (Vue 3) - A drag-and-drop Kanban board for managing recommendations
- **Database** - Stores employees, recommendations, and assignments (currently in-memory for demo)

## Getting Started

### Prerequisites

You'll need:

- .NET 9.0 SDK
- Node.js 18+
- Visual Studio 2022 or VS Code (or any editor you prefer)

### Running the Backend

1. **Navigate to the API folder**

   ```bash
   cd MskSuggestionManagement.Api
   ```

2. **Restore packages**

   ```bash
   dotnet restore
   ```

3. **Run the API**

   ```bash
   dotnet run
   ```

4. **Check it's working**
   - API: `https://localhost:7230/api`
   - Swagger docs: `https://localhost:7230/swagger`
   - Health check: `https://localhost:7230/health`

### Running the Frontend

1. **Navigate to the frontend folder**

   ```bash
   cd MskManagementBoard.FrontEnd
   ```

2. **Install dependencies**

   ```bash
   npm install
   ```

3. **Start the dev server**

   ```bash
   npm run dev
   ```

4. **Open your browser**
   - Frontend: `http://localhost:5173`
   - Make sure the backend is running first!

## What I Built

### Backend (API)

**Technologies I used:**

- **.NET 9** - Latest version, good performance
- **Entity Framework Core** - ORM that actually works well
- **AutoMapper** - Maps between DTOs and entities without writing boring mapping code
- **Swagger** - Auto-generates API documentation

**API Endpoints:**

- `GET /api/employees` - Get all employees
- `POST /api/employees` - Add a new employee
- `GET /api/mskrecommendations` - Get all recommendations for the Kanban board
- `GET /api/mskrecommendations/vida` - Get recommendations from Vida system
- `GET /api/mskrecommendations/{employeeId}/employee` - Get all assignments for an employee
- `POST /api/mskrecommendations/assign` - Assign a recommendation to someone
- `PUT /api/mskrecommendations/update-status` - Update assignment status

### Frontend (Vue App)

**Technologies I used:**

- **Vue 3** - I went with Vue because I like how it handles reactivity and the component system
- **TypeScript** - Added this for better type safety and fewer runtime errors
- **Vite** - Super fast build tool, much better than webpack for development
- **Syncfusion Kanban** - This was the best Kanban component I could find that actually works well
- **Axios** - For making API calls to the backend
- **Vue Toast Notification** - Shows nice notifications when users try to drag unassigned cards

**What the UI does:**

- Drag and drop cards between status columns (New, Assigned, InProgress, Completed, Rejected)
- Assign recommendations to employees by double-clicking cards
- Filter the board by employee using a dropdown
- Shows toast notifications when you try to drag unassigned "New" cards

## Database Design

### What I'm Using Now

I'm using Entity Framework Core with an in-memory database for the demo. It's not production-ready, but it works for showing how the system would work.

### How the Database Works

I kept the database structure pretty simple with just 3 main tables:

**Employees Table**

- `Id` (GUID) - Unique identifier for each employee
- `FirstName` (string, max 100 chars) - Employee's first name
- `LastName` (string, max 100 chars) - Employee's last name
- `Email` (string, max 255 chars) - Employee's email address (must be unique)
- `CreatedAt` (DateTime) - When the employee record was created
- Nothing fancy here, just the basics you need to know who's who

**MskRecommendations Table**

- `Id` (GUID) - Unique identifier for each recommendation
- `Title` (string, max 200 chars) - Short title for the recommendation
- `Description` (string, max 1000 chars) - Detailed description of what to do
- `Type` (enum) - What kind of recommendation (TargetedExercise, WorkspaceAdjustment, etc.)
- `Level` (enum) - Priority level (Low, Medium, High)
- `CreatedAt` (DateTime) - When the recommendation was created
- This is where all the MSK recommendations live

**EmployeeMskRecommendations Table**

- `EmployeeId` (GUID) - References the employee
- `MskRecommendationId` (GUID) - References the recommendation
- `Status` (enum) - Current status (New, Assigned, InProgress, Completed, Rejected)
- `AssignedAt` (DateTime, nullable) - When it was assigned to the employee
- This is the "glue" that connects employees to recommendations - it's a many-to-many relationship

### The Enums I Used

**Type** - What kind of recommendation it is:

- TargetedExercise
- WorkspaceAdjustment
- BehavioralChange
- LifestyleChange

**Level** - How urgent it is:

- Low
- Medium
- High

**Status** - Where it is in the process:

- New (just created, nobody assigned yet)
- Assigned (given to someone)
- InProgress (they're working on it)
- Completed (all done!)
- Rejected (they said no thanks)

### How I'd Structure a Real Database

For production, I'd use SQL Server with proper tables and relationships. Here's how I'd think about it:

**Employees Table**
I'd store basic employee information with a unique ID, their name, email (which has to be unique), and maybe their department. I'd also track when the record was created - just the basics you need to know who's who.

**MSK Recommendations Table**
This is where all the recommendations from VIDA would live. Each one gets a unique ID, has a type , a priority level, and the actual description of what to do.

**Employee Assignments Table**
This is the important one - it connects employees to recommendations. It stores which employee got which recommendation, what the current status is, when it was assigned, and when they finished it. I'd also allow for notes in case there's something important to remember about the assignment.

**Performance Considerations**
I'd add indexes on the fields I query most often - like looking up assignments by employee, filtering by status, or finding recommendations by source. This makes the database much faster when you're dealing with thousands of records.

**Data Relationships**
The key thing is that one employee can have multiple recommendations, and one recommendation can be given to multiple employees. But each specific assignment is unique - you can't assign the same recommendation to the same person twice.

## My Assumptions

### Business Rules

- **Assignment Model**: Each MSK suggestion is assigned to exactly one employee at a time (no shared assignments)
- **Workflow**: Simple linear progression: New → Assigned → InProgress → Completed/Rejected
- **Card Management**: Once a card is assigned, it can only be reassigned by changing the assignee
- **Status Updates**: Only assigned employees can update their own card status (admin can override)
- **Card Creation**: New recommendations come from VIDA system, not user-created

### UX & Behavior Choices

- **Kanban Columns**: Fixed columns (New, Assigned, InProgress, Completed, Rejected)
- **Drag & Drop**: Only allow dragging between adjacent columns
- **Card Assignment**: Double-click to assign, prevent dragging unassigned "New" cards
- **Filtering**: Simple employee dropdown filter, no complex search
- **Notifications**: Toast notifications for user feedback, no email notifications

## Architectural Decisions

### Why Clean Architecture?

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

### Frontend Architecture

I chose **Vue 3 with Composition API** because:

- Vue's reactivity system just makes sense to me - when data changes, the UI updates automatically
- Component-based architecture keeps things organized and reusable
- TypeScript catches bugs before they hit production
- Vite is lightning fast for development - no more waiting for builds

### State Management

I kept it simple with **local component state** instead of Vuex or Pinia because:

- The app isn't that complex - most data comes from API calls
- I didn't want to over-engineer it with a state management library
- Keeps the code easier to understand and debug

## What I'd Do With More Time

### Backend Improvements

- **Authentication & Authorization** - JWT tokens with proper roles
- **Real Database** - SQL Server with proper migrations
- **Caching** - Redis for frequently accessed data
- **Logging** - Serilog with correlation IDs for debugging
- **Unit/Integration Tests** - Actually test my code (I know, I should have)
- **API Versioning** - So I don't break things when making changes
- **Rate Limiting** - Prevent API abuse
- **Input Validation** - FluentValidation for comprehensive validation
- **Audit Trail** - Add base entity for audit trail like updatedAt, updatedBy

### Business Features

- **Bulk Operations** - Select multiple cards and do stuff to them
- **Advanced Filtering** - Filter by type, level, etc.
- **Reporting** - Show completion rates and analytics
- **Notifications** - Email people when they get assigned something
