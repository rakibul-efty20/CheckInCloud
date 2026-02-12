# CheckInCloud 🏨☁️

CheckInCloud is a modern, scalable **ASP.NET Core Web API** designed to manage hotel listings and their associated countries.  
The project follows **clean architecture principles** and implements advanced backend patterns to ensure **maintainability, security, and performance**.

---

## 📌 Project Overview

This API serves as a backend directory for managing hotel listings and country data.  
It leverages industry-standard architectural patterns to ensure **separation of concerns**, **data integrity**, and **secure access**.

---

## 🚀 Key Features

### 🔹 RESTful Architecture
- Fully compliant with REST principles  
- Supports standard HTTP methods: **GET, POST, PUT, DELETE**

### 🔹 Repository Pattern
- Abstracts data access logic  
- Promotes loose coupling and easier unit testing

### 🔹 Generic Repository
- Reusable base repository for common CRUD operations  
- Reduces code duplication and improves consistency

### 🔹 Unit of Work Pattern
- Coordinates multiple repositories  
- Ensures transactional consistency across database operations

### 🔹 Secure Authentication & Authorization
- Built with **ASP.NET Core Identity**
- **JWT Bearer Tokens** for authentication
- Role-based access control (RBAC)

### 🔹 Data Validation
- Uses **Data Annotations**
- Model state validation to ensure data integrity

### 🔹 DTO & AutoMapper
- Implements **Data Transfer Objects (DTOs)**
- Protects domain entities from over-posting
- AutoMapper for clean object mapping

### 🔹 Global Error Handling
- Centralized exception-handling middleware
- Consistent and user-friendly API error responses

### 🔹 Advanced Querying
- Server-side **pagination**
- **Filtering** and **sorting**
- Optimized for large datasets

### 🔹 API Documentation
- Integrated with **Scalar**
- Interactive API testing and exploration

---

## 🛠️ Tech Stack

| Category | Technology |
|-------|-----------|
| **Framework** | .NET 8.0 / .NET 9.0 (ASP.NET Core Web API) |
| **Language** | C# |
| **Database** | Microsoft SQL Server |
| **ORM** | Entity Framework Core (Code-First) |
| **Authentication** | ASP.NET Core Identity & JWT |
| **Mapping** | AutoMapper |
| **Logging** | Serilog |
| **Documentation** | Scalar |

---

## 📂 Architecture Overview

- **Controllers** – Handle HTTP requests and responses  
- **DTOs** – Shape data for client-server communication  
- **Repositories** – Encapsulate database access logic  
- **Unit of Work** – Manages transactional consistency  
- **Services** – Business logic layer  
- **Middleware** – Global exception handling & logging  

---

## 🔐 Security

- Password hashing via ASP.NET Core Identity  
- JWT-based authentication  
- Role-based authorization for protected endpoints  

---


