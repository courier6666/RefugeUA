# RefugeUA

**RefugeUA** is a web application built with **ASP.NET Core** (backend) and **Angular** (frontend). It helps demobilized soldiers, veterans, reservists, and their families find housing, employment, educational opportunities, and psychological support, while also fostering volunteer activities through events and gatherings. The platform supports social adaptation by providing tools for searching accommodation, jobs, training or retraining programs, and accessing mental health resources. It also enables volunteer organizations to coordinate their work, create groups, post volunteer event announcements, and recruit participants.

## Features
- 🏠 **Housing search** – find available accommodation
- 💼 **Employment assistance** – search for jobs and retraining opportunities
- 🎓 **Education and retraining** – access training programs and educational resources
- 💚 **Psychological support** – resources and contacts for mental health assistance
- 🤝 **Volunteer events** – volunteer organizations can create groups, post event announcements, and recruit participants

## Technologies
- **Backend:** ASP.NET Core
- **Frontend:** Angular
- **Database:** MS SQL Server
- **ORM:** Entity Framework Core
- **Architecture:** Vertical Slices Architecture

## Architecture
The **Vertical Slices Architecture** organizes the code around independent functional features—such as creating announcements or managing volunteer events. Each feature is implemented as a self-contained module that includes API endpoints, business logic, and data access. This approach simplifies maintenance and enhances scalability.

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Node.js & npm](https://nodejs.org/)
- [MS SQL Server](https://www.microsoft.com/sql-server)

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/<your-username>/refugeua.git
   cd refugeua
   ```
2. **Configure the database:**
   Update the connection string in the backend configuration and apply migrations:
   ```bash
   dotnet ef database update
   ```
3. **Run the backend:**
   ```bash
   cd backend
   dotnet run
   ```
4. **Run the frontend:**
   ```bash
   cd frontend
   npm install
   npm start
   ```

### Access the Application
- Backend API: `https://localhost:<port>`
- Frontend UI: `http://localhost:4200`
