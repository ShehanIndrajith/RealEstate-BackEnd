# 🏠 RealEstate Backend API

A **Real Estate Marketplace Backend API** built with **ASP.NET Core Web API (.NET 8)** using a **Monolith + Layered Architecture** approach. This backend powers a modern real estate platform that manages users, agents, builders, properties, reviews, and payments, and exposes secure REST APIs consumed by a **Next.js frontend**.

> 🚧 **Note:** An Admin Dashboard is planned but not yet implemented. The project is currently hosted and tested on **localhost**.

---

## 🔗 Related Repository

* **Frontend (Next.js Client):**
  [https://github.com/ShehanIndrajith/RealEstate-Client.git](https://github.com/ShehanIndrajith/RealEstate-Client.git)

---

## 🚀 Key Features

> 🚧 **Project Status:** This project is **still under active development**.

### ✅ Currently Implemented

* User Registration
* User Login
* JWT-based authentication & session validation
* Role-based access control (Admin / Agent / Builder / User)
* Authorized Agent profile data retrieval (secured using JWT)

### 🛠 In Progress / Planned

* Agent management (full CRUD)
* Builder management
* Property listing & advanced search
* Reviews & ratings system
* Secure payments (Stripe)
* Image, document, and video uploads via Cloudinary
* Email notifications using EmailJS
* Admin Dashboard

---

## 🧱 Architecture

* **Architecture Style:** Monolith, Layered Architecture
* **Layers:**

  * API (Controllers)
  * Core (Domain Entities, Interfaces)
  * Infrastructure (EF Core, Repositories, External Services)
  * Shared (DTOs, Common Utilities)

This structure ensures:

* Clear separation of concerns
* Maintainable and testable codebase
* Easy future migration to microservices if needed

---

## 🛠 Tech Stack

### Backend

* **Framework:** ASP.NET Core Web API (.NET 8)
* **Language:** C#
* **ORM:** Entity Framework Core (LINQ)
* **Database:** SQL Server
* **Authentication:** JWT (JSON Web Tokens)
* **Password Hashing:** bcrypt
* **API Documentation:** Swagger / OpenAPI

### DevOps & Tooling

* **Containerization:** Docker
* **CI/CD:** GitHub Actions
* **Image Registry:** Docker Hub
* **Version Control:** Git & GitHub

### External Services

* **Cloudinary:** Image, document, and video storage
* **Stripe:** Payments
* **EmailJS:** Email notifications

---

## 📁 Project Structure

```text
D:\Real Estate Web\RealEstate.API\
├─ .GitHub/
│  └─ workflows/
│     └─ backend-ci.yml
├─ certs/
│  └─ aspnetapp.pfx
├─ RealEstate.API/
│  └─ Dockerfile
├─ RealEstate.Core/
├─ RealEstate.Infrastructure/
├─ RealEstate.Shared/
└─ RealEstate.sln
```

---

## 🔐 Authentication & Authorization

* JWT-based authentication
* Role-based authorization using policies and attributes
* Secure password storage using **bcrypt hashing**
* Token-based session handling for frontend integration

---

## 🗄 Database

* **Approach:** Entity Framework Core (Code-First)
* **Migrations:** EF Core Migrations
* **Querying:** LINQ

---

## ⚙ Configuration

The application uses environment-based configuration:

* `appsettings.json`
* `appsettings.Development.json`

### Sensitive Configurations

The following values are managed via configuration files or environment variables:

* Database connection string
* JWT secret key
* Cloudinary credentials
* Stripe API keys

> ⚠️ Never commit real secrets to source control.

---

## 📄 API Documentation (Swagger)

* Swagger UI is enabled for easy API exploration and testing
* Supports JWT authentication via Bearer tokens
* Helps frontend developers integrate APIs efficiently

---

## 🐳 Docker & CI/CD

### Docker

* The backend is fully containerized
* Docker image is built using a custom `Dockerfile`

### GitHub Actions CI Workflow

* Triggered on **push / merge to `main` branch**
* Steps include:

  * Restore dependencies
  * Build project
  * Build Docker image
  * Push image to **Docker Hub**

This ensures consistent and automated builds.

---

## 📌 Hosting Status

* Currently hosted and tested on **localhost**
* Cloud deployment can be easily added in the future (Azure / AWS / GCP)

---

## 🧪 Future Improvements

* Admin Dashboard
* Advanced property analytics
* Caching (Redis)
* Background jobs (Hangfire / Quartz)
* Role-based Admin APIs

---

## 👨‍💻 Author

**Shehan Indrajith**
Experienced .NET & Full Stack Developer

* GitHub: [https://github.com/ShehanIndrajith](https://github.com/ShehanIndrajith)

---

## 📜 License

This project is currently developed for learning and portfolio purposes. License can be added later if required.
