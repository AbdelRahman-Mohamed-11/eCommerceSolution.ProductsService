# 🛍️ Products Microservice (Modern N-Tier Architecture)

This project is part of an eCommerce solution built using a **modern N-Tier architecture**. It enforces strict separation of concerns, where the **Business Logic Layer is isolated from the Data Access Layer**. This architecture supports high maintainability, testability, and scalability.

---

## 📦 Project Structure

```
eCommerceSolution.ProductsService
│
├── ProductsMicroService.API               # API Layer (Presentation)
├── ProductsMicroService.BusinessLogic     # Business Logic (Application Layer)
└── ProductsMicroService.DataAccess        # Data Access Layer (Infrastructure)
```

---

## 🧭 Architectural Overview

- **Business Logic Layer** is independent of EF Core and MySQL — it only defines contracts, validations, and core business logic.
- **Data Access Layer** implements interfaces defined in Business Logic and uses EF Core for MySQL operations.
- **API Layer** uses **Minimal APIs** with proper middleware, validation, and response formatting.

---

## 🔧 Tech Stack

- **.NET 8**
- **ASP.NET Core Web API (Minimal APIs)**
- **Entity Framework Core (with MySQL)**
- **MediatR** – for CQRS pattern
- **FluentValidation** – for request validation
- **Swagger + Scalar UI** – for API documentation and interactive UI
- **Docker** – for containerization
- **Specification Pattern** – for clean and reusable query logic
- **Result Pattern** – for consistent success/failure handling
- **Global Exception Handler** – for centralized error handling

---

## 🗂️ Projects & Responsibilities

### ✅ `ProductsMicroService.API`
- Minimal APIs for routing
- Middleware: Logging, Exception Handling, Authentication
- Swagger + Scalar UI documentation setup
- Dependency injection configuration

### ✅ `ProductsMicroService.BusinessLogic`
- `Dtos/` – Data Transfer Objects
- `Entities/` – Business domain models
- `Interfaces/` – Contracts for services and repositories
- `ServiceContracts/` – Interfaces for application use cases
- `Specifications/` – Specification pattern implementation
- `Validators/` – FluentValidation logic for DTOs
- `Result Pattern` – Used for consistent response handling

### ✅ `ProductsMicroService.DataAccess`
- `Context/` – EF Core DbContext with MySQL provider
- `Repositories/` – EF Core implementation of business interfaces
- `Configurations/` – Fluent API model configurations
- `Migrations/` – Database migration scripts
- `UnitOfWork.cs` – Manages transaction scope
- `DependencyInjection.cs` – Infrastructure DI registration

---

## 🚀 Running the Application

### 1. Clone the repository:
```bash
git clone https://github.com/AbdelRahman-Mohamed-11/ProductsMicroService.git
cd ProductsMicroService
```

### 2. Setup your database (MySQL)
Make sure you have MySQL running and update your connection string in `appsettings.json`.

### 3. Run Migrations:
```bash
dotnet ef database update --project ProductsMicroService.DataAccess
```

### 4. Run the application:
```bash
dotnet run --project ProductsMicroService.API
```

### 5. Access Swagger/Scalar UI:
- Swagger UI: `http://localhost:<port>/swagger`
- Scalar UI: Configured at `/docs` or your custom route

### 6. Run in Docker:
```bash
docker build -t products-microservice -f ProductsMicroService.API/Dockerfile .
docker run -p 5000:80 products-microservice
```

---

## 📄 Swagger & Scalar UI Features

- `ProducesResponseType` – Annotated API responses
- JWT Bearer Authentication support
- Custom Scalar UI themes
- Developer-friendly interactive docs

---

## 🧰 Design Principles

- ✅ **Modern N-Tier architecture**
- ✅ **Dependency Inversion** (BL depends only on abstractions)
- ✅ **Separation of concerns**
- ✅ **Specification Pattern** for queries
- ✅ **Result Pattern** for clean success/error returns
- ✅ **Minimal APIs** for performance and simplicity
- ✅ **Global Exception Handling** for consistent error responses

---

## 🔮 Future Enhancements

- Add unit and integration testing with xUnit / NUnit
- Integrate CI/CD pipelines (GitHub Actions)
- Add distributed caching (Redis)
- Introduce health checks and Prometheus metrics
- Setup structured logging with Serilog + Seq

---

## 👨‍💻 Author

Built with ❤️ by [Abdelrahman Mohamed](https://github.com/AbdelRahman-Mohamed-11)
