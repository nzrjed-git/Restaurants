# 🍽️ Restaurants API

A robust, scalable, and maintainable RESTful API for managing restaurant-related data, built using **ASP.NET Core 8**, **Clean Architecture**, and deployed to **Azure**. This project reflects practical implementation of concepts learned in the [ASP.NET Core Web API - Clean Architecture & Azure](https://www.udemy.com/course/aspnet-core-web-api-clean-architecture-azure/) course.

## 📝 Overview

The Restaurants API provides endpoints to manage restaurants, menus, and related entities. It adheres to RESTful principles and employs Clean Architecture to ensure separation of concerns, testability, and scalability.

## 🛠️ Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **MediatR** (CQRS pattern)
- **FluentValidation**
- **Serilog**
- **AutoMapper**
- **Swagger / Swashbuckle**
- **Microsoft Identity**
- **Microsoft IdentityServer**
- **Application Insights**
- **Azure App Service & Azure SQL**
- **xUnit** (Testing)

## 🧩 Architecture

The project follows the **Clean Architecture** paradigm, separating the solution into:

- **Domain**: Core business logic and domain entities.
- **Application**: Use cases, DTOs, interfaces, and validation logic.
- **Infrastructure**: Persistence, external services, and configurations.
- **Presentation (API)**: HTTP endpoints, controllers, and filters.

This architecture improves code maintainability, testability, and long-term flexibility.

## 🚀 Features & Learning Outcomes

This project demonstrates and reinforces the following skills and concepts:

- ✅ **RESTful API design** with proper HTTP verbs and status codes.
- ✅ **Clean Architecture** implementation for decoupled, layered development.
- ✅ **Entity Framework Core** for data access and migrations.
- ✅ **CQRS pattern with MediatR** to separate read/write concerns.
- ✅ **DTOs and AutoMapper** to handle object transformation.
- ✅ **Validation with FluentValidation** to enforce input rules.
- ✅ **Serilog logging** for structured diagnostics.
- ✅ **Global exception handling** with consistent API error responses.
- ✅ **Interactive API documentation** using Swagger.
- ✅ **Microsoft Identity & IdentityServer** integration:
  - 🔐 **Role-based authorization**
  - 🔐 **Claims-based authorization using policies**
  - 🔐 **Resource-based authorization** for fine-grained access control
- ✅ **Authentication with JWT bearer tokens**
- ✅ **Pagination and sorting** for large datasets.
- ✅ **Monitoring and telemetry** with **Application Insights**.
- ✅ **Automated unit & integration testing** with xUnit.
- ✅ **Azure deployment** using App Service & Azure SQL.
- ✅ **CI/CD pipelines** for automated deployments (course demo).

## 🧠 Design Decisions

- ✅ **Clean Architecture** for scalable, testable design.
- ✅ **MediatR** for decoupled business logic.
- ✅ **IdentityServer** for flexible and secure authentication/authorization.
- ✅ **FluentValidation** and **DTOs** for data safety and clarity.
- ✅ **Serilog** and **Application Insights** for powerful logging and monitoring.
- ✅ **Swagger** for API discoverability and testing.
