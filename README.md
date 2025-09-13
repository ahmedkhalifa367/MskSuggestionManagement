### Build an MSK Suggestion Management Board

**Background**

A regional director of health, safety and wellbeing at a client has been tasked with reducing rates of MSK absence in the company. They've rolled out VIDA to help them achieve it. Now they need to ensure employees are reducing their risk based on suggestions made by VIDA.

The system handles four main types of recommendations:

1. **Targeted Exercises** - Things like strength training, flexibility work, balance exercises
2. **Workspace Adjustments** - Monitor height changes, ordering ergonomic equipment
3. **Behavioral Changes** - Taking micro breaks, improving posture
4. **Lifestyle Changes** - Better sleep habits, exercise routines, diet improvements

## Tech Stack

### Frontend

I went with Vue 3 and TypeScript because I wanted something modern and type-safe. For the Kanban board, I used Syncfusion's component library - it's really solid and handles drag-and-drop beautifully. Vite handles the build process, and I wrote custom CSS for the styling.

### Backend

I built the API using .NET 9 with Clean Architecture. I separated everything into layers (Domain, Application, Infrastructure, API) to keep things organized. Entity Framework Core handles the data access with an in-memory database as requested. Swagger is set up for API documentation.

## Getting Started

### Prerequisites

- Node.js (v20+)
- .NET 9 SDK
- Git

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/YOUR_USERNAME/msk-suggestion-management-board.git
   cd msk-suggestion-management-board
   ```

2. **Backend Setup:**

   ```bash
   cd MskSuggestionManagement.Api
   dotnet restore
   dotnet run --project MskSuggestionManagement.Api
   ```

3. **Frontend Setup:**
   ```bash
   cd MskManagementBoard.FrontEnd
   npm install
   npm run dev
   ```

You'll need Node.js (v20+) and .NET 9 SDK installed. I used VS Code for development, but Visual Studio works too.

### Running the Backend

First, let's get the API running:

```bash
cd MskSuggestionManagement.Api
dotnet restore
dotnet run --project MskSuggestionManagement.Api
```

Once it's running, you can check out:

- API: `https://localhost:7230/api`
- Swagger docs: `https://localhost:7230/swagger`

### Running the Frontend

In a new terminal:

```bash
cd MskManagementBoard.FrontEnd
npm install
npm run dev
```

The frontend will be at `http://localhost:5173`

## Frontend Details

I built the frontend with Vue 3 and TypeScript. Here's what I used:

- Vue 3.5.18 with the Composition API
- TypeScript for better development experience
- Vite for fast builds and hot reload
- Syncfusion Kanban component (it's really good!)
- Vue Facing Decorator for class-based components

### What I Built

The main feature is the interactive Kanban board where you can:

- Drag cards between different status columns
- See real-time updates when you move things around
- View detailed information in popup dialogs
- Assign recommendations to employees with a dropdown

Each card shows the recommendation title, description, risk level (with color-coded badges), and who it's assigned to. The board is fully responsive and works on mobile too.

## Backend Details

For the backend, I used .NET 9 with Clean Architecture. Here's the stack:

- ASP.NET Core Web API for the REST endpoints
- Entity Framework Core with in-memory database (as requested)
- AutoMapper for object mapping
- Swagger for API documentation

### API Endpoints

I created these endpoints:

**Employee Management:**

- `GET /api/employees` - Get all employees
- `POST /api/employees` - Add new employee

**MSK Recommendations:**

- `GET /api/msk-recommendations` - Get all recommendations for the Kanban board
- `GET /api/msk-recommendations/vida` - Get unassigned VIDA recommendations
- `GET /api/msk-recommendations/{employeeId}/employee` - Get recommendations for a specific employee
- `POST /api/msk-recommendations/assign` - Assign a recommendation to an employee
- `PUT /api/msk-recommendations/update-status` - Update the status of an assignment

## Database Design

I kept the database simple but effective:

**Employee Table:**

- Id, FirstName, LastName, Email, timestamps

**MskRecommendation Table:**

- Id, Title, Description, Type (enum), Level (enum), timestamps

**EmployeeMskRecommendation Table (junction):**

- EmployeeId, MskRecommendationId, Status (enum), timestamps

The junction table handles the many-to-many relationship between employees and recommendations, with a status field to track progress.

## Running the Project

Make sure you have Node.js (v20+) and .NET 9 SDK installed.

**Backend:**

```bash
cd MskSuggestionManagement.Api
dotnet restore
dotnet run --project MskSuggestionManagement.Api
```

**Frontend:**

```bash
cd MskManagementBoard.FrontEnd
npm install
npm run dev
```

The API will be at `https://localhost:7230` and the frontend at `http://localhost:5173`.
