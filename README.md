# RefugeUA 🇺🇦

RefugeUA is a full-stack web application designed to support **internally displaced persons (IDPs), veterans, reservists, and their families** by providing a centralized platform for housing, employment, education, psychological support, and volunteer initiatives.

The project is developed as an **academic (diploma-level) system** and follows modern software engineering principles, clean architecture, and scalable design practices.

---

## 🎯 Project Goal

The main goal of RefugeUA is to create a secure, modular, and extensible information platform that:

- Helps vulnerable groups find housing, jobs, and education opportunities  
- Enables volunteers and community representatives to organize and publish events  
- Provides administrators with moderation and management tools  
- Supports role-based access and secure authentication  

---

## 🧱 Technology Stack

### Backend
- **ASP.NET Core**
- **C#**
- **Entity Framework Core**
- **MS SQL Server**
- **FluentValidation**
- **JWT Authentication**
- **REST API**

### Frontend
- **Angular**
- **TypeScript**
- **HTML / CSS**
- **Angular HttpClient**
- **RxJS**

---

## 🏗️ Overall Architecture

RefugeUA follows a **client–server architecture** with strict separation of concerns.

The system consists of:

1. **Angular Client (SPA)**
2. **ASP.NET Core Web API**
3. **Application & Domain Logic (Vertical Slices)**
4. **Infrastructure Layer (Database & File Storage)**

---

## 🧩 Architecture Diagram

```
┌──────────────────────────────────────────┐
│            Client (Angular)              │
│                                          │
│  • UI Components                         │
│  • Templates                             │
│  • Data Models                           │
│  • Services                              │
│  • API Module                            │
│                                          │
│  JSON over HTTP                          │
└───────────────▲──────────────────────────┘
                │
                ▼
┌──────────────────────────────────────────┐
│          Server (ASP.NET Core)           │
│                                          │
│  • API Endpoints (Controllers)           │
│  • Authentication & Authorization (JWT)  │
│  • Validation (FluentValidation)         │
│  • File Management Service               │
│                                          │
│  Application Layer (Vertical Slices)     │
│  • Commands / Queries                    │
│  • Feature Handlers                      │
│  • DTOs                                  │
│                                          │
│  Infrastructure Layer                    │
│  • Entity Framework Core                 │
│  • SQL Queries                           │
└───────────────▲──────────────────────────┘
                │
                ▼
┌──────────────────────────────────────────┐
│        Database (MS SQL Server)          │
│                                          │
│  • Users & Roles                         │
│  • Advertisements                        │
│  • Volunteer Events                      │
│  • Responses & Feedback                  │
└──────────────────────────────────────────┘

┌──────────────────────────────────────────┐
│     Static Files / wwwroot               │
│  • Uploaded images                       │
│  • Documents                             │
└──────────────────────────────────────────┘
```

---

## 🧠 Backend Architecture

### Vertical Slices Architecture

The backend is organized using **Vertical Slices Architecture**, where each feature is implemented as an independent slice containing:

- API endpoint
- Request/response models (DTOs)
- Validation logic
- Business rules
- Data access logic

---

## 🗃️ Data Access

Entity Framework Core is used with a **code-first approach**:

- Strongly typed entities
- LINQ-based queries
- Database migrations
- Clear separation between domain models and persistence

The database schema includes:
- Users and roles
- Advertisements (housing, jobs, education)
- Volunteer events
- Responses and feedback

---

## 🔐 Authentication & Authorization

- JWT-based authentication
- Role-based authorization

Supported roles:
- Refugee / Soldier / Family Member
- Volunteer
- Community Representative
- Administrator

---

## 📂 File Management

The system includes a dedicated file service that:

- Handles file uploads (images, documents)
- Stores files in `wwwroot`
- Saves file paths in the database
- Separates file metadata from physical storage

---

## 📚 Key Libraries Used

### Backend Libraries
- Microsoft.AspNetCore.App
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- FluentValidation
- System.IdentityModel.Tokens.Jwt

### Frontend Libraries
- @angular/core
- @angular/router
- @angular/common/http
- rxjs

---

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

## UI
<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/MainPage.jpg?raw=true" alt="Main Page" width="900"/>
</p>
<p align="center">Main Page</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageAdvertsSearch.jpg?raw=true" alt="Search Adverts" width="900"/>
</p>
<p align="center">Search Adverts</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageAdvert1.jpg?raw=true" alt="Advert Page 1" width="900"/>
</p>
<p align="center">Advert Page 1</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageAdvert2.jpg?raw=true" alt="Advert Page 2" width="900"/>
</p>
<p align="center">Advert Page 2</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/AdvertResponseForm.jpg?raw=true" alt="Advert Response Form" width="900"/>
</p>
<p align="center">Advert Response Form</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/OwnResponsesViewPage.jpg?raw=true" alt="User Responses Page" width="900"/>
</p>
<p align="center">User Responses Page</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageViewPsychologicalHelp1.jpg?raw=true" alt="Psychological Help Page" width="900"/>
</p>
<p align="center">Psychological Help Page</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageViewPsychologicalHelp2.jpg?raw=true" alt="Psychological Help Details" width="900"/>
</p>
<p align="center">Psychological Help Details</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageVolunteerEvent1.jpg?raw=true" alt="Volunteer Event Page" width="900"/>
</p>
<p align="center">Volunteer Event Page</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageVolunteerEvent2.jpg?raw=true" alt="Volunteer Event Details" width="900"/>
</p>
<p align="center">Volunteer Event Details</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageVolunteerEvents.jpg?raw=true" alt="Volunteer Events List" width="900"/>
</p>
<p align="center">Volunteer Events List</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageVolunteerGroup1.jpg?raw=true" alt="Volunteer Group Page" width="900"/>
</p>
<p align="center">Volunteer Group Page</p>

<p align="center">
  <img src="https://github.com/courier6666/RefugeUA/blob/main/images/PageVolunteerGroup2.jpg?raw=true" alt="Volunteer Group Details" width="900"/>
</p>
<p align="center">Volunteer Group Details</p>