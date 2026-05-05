# DotCruz.CoreAuth Project

## Project Overview
DotCruz.CoreAuth is a modular authentication core system built with **.NET 10.0** and **Clean Architecture**. It follows **Domain-Driven Design (DDD)** principles to provide a robust and maintainable foundation for user management and authentication processes.

### Technologies
- **Runtime:** .NET 10.0
- **Language:** C#
- **ORM:** Entity Framework Core (EF Core)
- **Database:** PostgreSQL (via Npgsql)
- **Naming Convention:** snake_case (managed via `EFCore.NamingConventions`)
- **Testing Framework:** xUnit

### Architecture
The project is divided into several layers according to Clean Architecture:
- **DotCruz.CoreAuth.Domain:** Contains core business logic, entities (`User`, `RefreshToken`, `PasswordResetToken`), enums, and repository interfaces.
- **DotCruz.CoreAuth.Application:** Implements use cases, commands, and queries (e.g., `CreateUserCommand`).
- **DotCruz.CoreAuth.Infrastructure:** Handles external concerns like data access (EF Core), repository implementations, Unit of Work, and dependency injection configuration.
- **DotCruz.CoreAuth.Exceptions:** Centralized exception handling and localized resource messages (`ResourceMessagesException`).
- **DotCruz.CoreAuth.Common:** Shared utilities, settings (e.g., `JwtTokenSettings`), and common logic.

## Building and Running

### Prerequisites
- .NET 10 SDK or later.
- PostgreSQL database.

### Key Commands
- **Build the solution:**
  ```bash
  dotnet build
  ```
- **Run tests:**
  ```bash
  dotnet test
  ```
- **Run (TODO):** Currently, the solution contains library and infrastructure layers. An entry-point project (e.g., Web API) is not yet present in the `src` directory.

## Development Conventions

### Domain-Driven Design (DDD)
- **Entities:** Entities like `User` use private setters and expose public methods/constructors for state changes.
- **Validation:** Business rule validation is performed directly within entities (e.g., `User.Validate()`), throwing custom exceptions like `ErrorOnValidationException` when rules are violated.

### Data Access
- **Repository Pattern:** Interfaces for reading and writing are split (e.g., `IUserReadRepository`, `IUserWriteRepository`).
- **Unit of Work:** Used to coordinate changes across multiple repositories.
- **Soft Delete:** Implemented via a `BaseEntity` with a `DeletedAt` property. A Global Query Filter in `DotCruz.CoreAuthDbContext` automatically excludes soft-deleted records.
- **Snake Case:** All database tables and columns use `snake_case` naming conventions.

### Error Handling
- Use the custom exceptions defined in `DotCruz.CoreAuth.Exceptions.BaseExceptions`.
- Localized messages are stored in `.resx` files and accessed via `ResourceMessagesException`.

### Testing
DotCruz.CoreAuth follows a comprehensive testing strategy focused on reliability and maintainability.

#### Tools & Libraries
- **xUnit:** The primary testing framework.
- **Moq:** Used for mocking dependencies (repositories, unit of work, etc.).
- **Bogus:** Employed to generate realistic test data (names, emails, passwords).

#### Test Categories
- **Domain.Test:** Unit tests for domain entities. They verify business rules, internal state changes, and self-validation logic.
- **Validators.Test:** Tests for command/query validators (FluentValidation). They ensure that input data meets the required constraints before reaching use cases.
- **Command.Test:** Tests for application layer handlers (commands). These tests orchestrate the interaction between domain logic and infrastructure, using mocks for external dependencies.
- **CommonTestUtilities:** A shared project containing builders and helpers to simplify test setup and promote reuse.

#### Key Patterns & Conventions
- **Test Data Builders:** Centralized in `CommonTestUtilities`, builders (e.g., `UserBuilder`, `CreateUserCommandBuilder`) provide a fluent API for creating data with default valid values or specific overrides.
- **Service/Handler Builders:** Use case handlers and repository mocks are also created via builders (e.g., `CreateUserCommandHandlerBuilder`, `UserReadRepositoryBuilder`). This abstracts away the complexity of Moq setup.
- **Naming Convention:** Tests generally follow the naming pattern:
  - `Success`: For the happy path.
  - `Error_[Reason]`: For failure scenarios (e.g., `Error_Email_Already_Registered`).
- **Validation Message Verification:** Always verify that the error messages returned by the system match the expected localized keys in `ResourceMessagesException`.
- **Failure Path Coverage:** Explicitly test for expected exceptions (e.g., `ErrorOnValidationException`, `NotFoundException`) and multiple validation errors.

#### Running Tests
Execute all tests from the root directory using the .NET CLI:
```bash
dotnet test
```
