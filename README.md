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