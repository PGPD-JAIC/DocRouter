# DocRouter API Reference
Use the pane on the left to browse the documentation for the APIs in this project.

## Namespaces
We're attempting to apply Clean Architecture principals in the structure of this project. We've modeled the structure of the application based on the recommendations in [Jason Taylor's Northwind Application](https://github.com/jasontaylordev/NorthwindTraders).
The project is currently separated into 6 separate namespaces:

### Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Domain
This will contain all entities, enums, exceptions, types and logic specific to the domain. The Entity Framework related classes are abstract, and should be considered in the same light as .NET Core.

### Common
This will contain all cross-cutting concerns.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Persistence
This layer will contain all EF-specific implementations.

### WebUI
This layer will contain the MVC Core web application user interface.