Tattoo Salon Web API

REST API для автоматизації бізнес-процесів тату-салону. Проєкт побудований з використанням Clean Architecture та CQRS, що забезпечує розділення відповідальностей, масштабованість та зручність підтримки коду.

Project Highlights
Clean Architecture
CQRS with MediatR
JWT Authentication + Refresh Tokens
Redis Distributed Cache
Hangfire Background Jobs
PostgreSQL + Entity Framework Core
FluentValidation
Global Exception Handling
Optimistic Concurrency Control
Docker & Docker Compose
Unit & Integration Testing
Architecture
Controllers
    ↓
MediatR
    ↓
Commands / Queries
    ↓
Pipeline Behaviors
    ↓
Handlers
    ↓
Repositories
    ↓
Entity Framework Core
    ↓
PostgreSQL

Application Layer
Містить бізнес-логіку, CQRS-команди та запити, валідатори, DTO та пайплайни MediatR.

Infrastructure Layer
Відповідає за роботу з базою даних, кешем, email-сервісом, PDF-генерацією та іншими зовнішніми залежностями.

Domain Layer
Містить сутності предметної області та бізнес-моделі.

API Layer
Контролери, middleware та конфігурація застосунку.

Tech Stack
Backend
ASP.NET Core Web API
C#
REST API
Database
PostgreSQL
Entity Framework Core
Code First Migrations
Architecture
Clean Architecture
CQRS
MediatR
Repository Pattern
Unit Of Work
Specification Pattern
Security
JWT Authentication
Refresh Tokens
Role-Based Authorization
Validation
FluentValidation
Caching
Redis
Distributed Cache
MediatR Pipeline Behaviors
Background Jobs
Hangfire
Logging
Serilog
DevOps
Docker
Docker Compose
Testing
xUnit
Moq
FluentAssertions
Integration Tests
Unit Tests
Features
Authentication
User Registration
Login
JWT Access Tokens
Refresh Tokens
Logout
Role-Based Authorization
Tattoo Management
Create Tattoo
View Tattoo Catalog
Pagination Support
Artist Management
Create Artist
View Artists
Filter Artists by Price
Appointment Management
Create Appointment
View Appointments
Delete Appointments
Performance
Redis Distributed Cache
Pagination
Optimized Database Queries
Reliability
Global Exception Handling
Validation Pipeline
Concurrency Control (RowVersion)
Background Processing
Automatic Cleanup Jobs
Email Notifications
PDF Report Generation
API Documentation

Після запуску проєкту Swagger буде доступний за адресою:

http://localhost:5000/swagger
Running with Docker
Clone Repository
git clone https://github.com/Dimab-b/TattooSalon-API-.git
cd TattooSalon-API-
Configure Environment Variables

Створіть файл .env у корені проєкту:

DB_USER=postgres
DB_PASSWORD=your_password
Configure Application Settings

У файлі appsettings.json необхідно вказати:

PostgreSQL connection string
JWT secret key
SMTP settings for email sending
Start Application
docker-compose up --build

Після запуску будуть автоматично підняті:

ASP.NET Core API
PostgreSQL
Redis
Також автоматично застосуються EF Core міграції.
Як отримати права адміністратора:
При звичайній реєстрації через ендпоінт API створюється користувач із базовими правами клієнта. Для доступу до захищених функцій управління салоном потрібно змінити роль безпосередньо в базі даних. Підключіться до вашої бази PostgreSQL (наприклад, через pgAdmin або DBeaver) і виконайте цей SQL-запит:
UPDATE "Users" SET "Role" = 'Admin' WHERE "Username" = 'тут_логін_вашого_користувача';

Database Migrations

Створення нової міграції:
dotnet ef migrations add MigrationName
Застосування міграцій:
dotnet ef database update
Testing

Запуск усіх тестів:
dotnet test

Проєкт містить:
Unit Tests
Integration Tests
Mocking через Moq
Assertions через FluentAssertions
Security

У проєкті реалізовано:

JWT Authentication
Refresh Tokens
Role-Based Authorization
Custom Authorization Policies
Secure HTTP-Only Cookies для Refresh Token
