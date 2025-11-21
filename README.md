Hospital Management System API

ASP.NET Core Web API + JWT Authentication + Redis Token Blacklisting

A secure, scalable, and modern Hospital Management backend built using ASP.NET Core Web API, Entity Framework Core, JWT Authentication, and Redis for token blacklisting.
Includes full CRUD for Doctors & Patients with protected API routes.

🚀 Features
🔐 Authentication

User Registration

User Login (returns JWT token)

Protected routes using [Authorize]

Logout system using Redis token blacklist

Custom JWT blacklist middleware

⚡ Redis Integration

Stores invalidated (logged-out) JWT tokens

Middleware checks token status before every request

High-performance caching support

🏥 Hospital Management

CRUD for Doctors

CRUD for Patients

User Model

Validation + error responses

🧰 Tech Stack

ASP.NET Core Web API

MySQL + EF Core

Redis (StackExchange.Redis)

Swagger UI

C# .NET 7/8
