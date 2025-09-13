# MSK Suggestion Management API

A comprehensive API for managing MSK (Musculoskeletal) recommendations and employee assignments, built with .NET Core and Clean Architecture principles.

## 🚀 Quick Start

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Entity Framework Core Tools

### Installation

1. Clone the repository
2. Navigate to the API project directory
3. Restore packages:
   ```bash
   dotnet restore
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

### Access Points

- **API Base URL**: `https://localhost:7230/api`
- **Swagger Documentation**: `https://localhost:7230/swagger`
- **Health Check**: `https://localhost:7230/health`

## 📚 Documentation

- [API Documentation](./docs/API_Documentation.md) - Complete API reference
- [System Architecture](./docs/System_Architecture.md) - Architecture overview
- [UML Diagrams](./docs/UML_Diagrams.md) - Visual system diagrams

## 📋 API Endpoints

### Employee Management

- `GET /api/employees` - Get all employees
- `POST /api/employees` - Add new employee

### MSK Recommendation Management

- `GET /api/mskrecommendations` - Get all recommendations for kaban board
- `GET /api/mskrecommendations/vida` - Get all recommendations from Vida
- `GET /api/mskrecommendations/assignments` - Get all assignments
- `POST /api/mskrecommendations/assign` - Assign recommendation to employee
- `PUT /api/mskrecommendations/status` - Update assignment status

## 🗄️ Database Schema

### Entities

- **Employee**: Basic employee information
- **MskRecommendation**: MSK recommendation details
- **EmployeeMskRecommendation**: Assignment relationship with status, the relation between the **Employee** and **MskRecommendation** is many to many.

### Enums

- **Type**: TargetedExercise, WorkspaceAdjustment, BehavioralChange, LifestyleChange
- **Level**: Low, Medium, High
- **Status**: New, Assigned, InProgress, Completed, Rejected

## 🔒 Security

### Current Configuration

- CORS enabled for development origins
- No authentication (development mode)
- Input validation in services



