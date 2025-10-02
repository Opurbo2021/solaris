# SolarCRM

A **full-stack CRM platform** designed for **solar energy companies** to manage customers, installations, equipment, energy production, and customer support.
This project demonstrates enterprise-grade **backend architecture** and **domain-driven design**, tailored for the renewable energy industry.


## 🌟 Project Overview

SolarCRM provides a structured solution for companies in the solar industry by covering the following:

* **Customer Management** → onboarding, document storage, and lifecycle tracking (Lead → Prospect → Active → Inactive).
* **Installation Management** → project workflow (Survey → Design → Permits → Installation → Inspection → Activation).
* **Technician Assignment** → manage and track crews working on installations.
* **Equipment Tracking** → inventory, assignment, warranties, and maintenance.
* **Energy Monitoring** → capture production data, compare expected vs. actual output, and log system health.
* **Support & Communication** → ticketing system with attachments, technician assignments, and resolution tracking.
* **Document Management** → unified file handling for customers, installations, and support cases.


## 🚀 Tech Stack

* **Backend:** ASP.NET Core (Clean Architecture + CQRS)
* **Database:** PostgreSQL with Entity Framework Core
* **Frontend:** React (TypeScript + TailwindCSS)
* **Authentication:** JWT-based role management (Admin, Technician, Customer)
* **Logging & Testing:** Serilog, xUnit, Moq, FluentAssertions

## 🏗️ Project Structure

```
SolarEnergyManagement/
├── Backend/
│   ├── Domain/                     # Core business entities
│   │   ├── Entities/
│   │   │   ├── Customer.cs
│   │   │   ├── Installation.cs
│   │   │   ├── EnergyProduction.cs
│   │   │   └── Technician.cs
│   │   ├── Enums/
│   │   │   ├── CustomerStatus.cs
│   │   │   ├── InstallationStatus.cs
│   │   │   └── SystemHealth.cs
│   │   └── ValueObjects/
│   ├── Application/                # Business logic and use cases
│   │   ├── Features/
│   │   │   ├── Customers/
│   │   │   │   ├── Commands/
│   │   │   │   └── Queries/
│   │   │   ├── Installations/
│   │   │   └── EnergyMonitoring/
│   │   └── Common/
│   │       ├── Interfaces/
│   │       ├── Behaviors/
│   │       └── Models/
│   ├── Infrastructure/             # Data access and external services
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Configurations/
│   │   ├── Services/
│   │   │   ├── EmailService.cs
│   │   │   └── FileStorageService.cs
│   │   └── Identity/
│   └── WebAPI/                     # API controllers and configuration
│       ├── Controllers/
│       │   ├── CustomersController.cs
│       │   ├── InstallationsController.cs
│       │   └── EnergyController.cs
│       └── Extensions/
├── tests/
│   ├── UnitTests/
│   └── IntegrationTests/
└── frontend/ (Future)
└── solar-dashboard-react/
```


## 📦 Core Entities & Relationships

### 👤 Customer

* Stores customer details (name, email, phone, address).
* Tracks **status** (`Lead`, `Prospect`, `Active`, `Inactive`).
* Linked to **installations**, **documents**, and **support tickets**.

### 📍 Address

* Reusable entity for **customer addresses** and **installation sites**.

### ⚡ Installation

* Represents a solar project with workflow stages (`Survey → Design → Installation → Activation`).
* Tracks **system specs** (size, panel count, inverter type).
* Linked to **customer**, **installation address**, **status history**, **equipment**, **technicians**, and **documents**.

### 📊 EnergyProduction

* Daily production metrics (`Actual vs Expected`).
* Linked to **installations** and optional **weather data**.
* Helps calculate performance and detect system health issues.

### 🔧 Equipment

* Inventory entity for panels, inverters, batteries, etc.
* Tracks **type**, **status** (`InStock`, `Installed`, `NeedsRepair`), **warranty**, and **costs**.
* Assignable to **installations**.

### 🛠️ Technician Assignment

* Many-to-many mapping of **users** (technicians) to **installations**.
* Tracks assignment date, role (Surveyor, Installer, Inspector), and completion status.

### 📑 Document

* Generic storage entity with **polymorphic links** to Customer, Installation, or SupportTicket.
* Supports various types: contracts, permits, photos, reports, etc.

### 🎫 SupportTicket

* Ticketing system for customer issues.
* Tracks **status** (`Open → InProgress → Resolved → Closed`) and **priority** (`Low → Critical`).
* Linked to **customer**, **installation**, **assigned user**, and **documents**.

### 👥 User

* Represents system users: **Admin**, **Technician**, or **Customer**.
* Stores authentication details and technician-specific info (specialization, license).
* Linked to **assigned installations** and **support tickets**.


## 📚 Enumerations

* **CustomerStatus:** Lead, Prospect, Active, Inactive
* **InstallationStatus:** Survey, Design, Permits, Installation, Inspection, Active, Deactivated
* **EquipmentType:** SolarPanel, Inverter, Battery, etc.
* **EquipmentStatus:** InStock, Assigned, Installed, NeedsRepair, Retired
* **SystemHealthStatus:** Excellent → Offline
* **TicketStatus / TicketPriority**
* **DocumentType:** Customer, Installation, Ticket, Generic
* **UserRole:** Admin, Technician, Customer


## Customer Journey
```
Lead → Prospect → Survey → Design → Permits → Installation → Active
```

## Energy Monitoring Flow
```
Installation → Energy Production → Performance Analysis → Billing → Reporting
```

### Installation Phases
1. **Survey** - Site assessment and requirements gathering
2. **Design** - System design and engineering
3. **Permits** - Regulatory approvals and documentation
4. **Installation** - Physical system installation
5. **Inspection** - Safety and compliance verification
6. **Activation** - System commissioning and monitoring setup


## 🚀 Getting Started

### 1. Prerequisites

* [.NET 9 SDK (or target SDK in `global.json`)](https://dotnet.microsoft.com/download)
* [PostgreSQL](https://www.postgresql.org/)
* [Node.js (v18+)](https://nodejs.org/) for frontend


### 2. Backend Setup
1. **Clone the Repository**

   ```bash
   git clone <project>
   cd solar-crm
   ```

2. **Set Up Configuration**
   Create a `.env` file in the project root and add the following values:

    * **Database Connection**

      ```env
      DEFAULTCONNECTION=Host=localhost;Database=SolarEnergyDb;Username=postgres;Password=yourpassword
      ```

    * **JWT Settings**

      ```env
      JWT_SECRET=your-jwt-secret-key-minimum-32-characters-long
      JWT_ISSUER=your-app-name
      JWT_AUDIENCE=your-app-users
      JWT_EXPIRY_MINUTES=jwt_lifetime
      JWT_REFRESH_TOKEN_LIFETIME_DAYS=refresh-token-lifetime
      ```

3. **Run Database Migrations**

   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
   ```

4. **Build and Run the Application**

   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/WebAPI
   ```

5. **Access the API**

    * API Documentation (Scalar): [https://localhost:5228/scalar/v1](https://localhost:5228/scalar/v1)


## 🤝 Contributing

This is a learning project, but contributions and suggestions are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

*Built with ☀️ for the clean energy future!*