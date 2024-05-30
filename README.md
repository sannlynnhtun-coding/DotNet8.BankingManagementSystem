## Banking Management System(Vertical Slice Architecture + NLayer Architecture)

Notes :

Database backup file is [here](BankingManagementSystem.sql).

-------

## Vertical Slice Architecture

Each feature in our application is developed as a vertical slice, cutting across all necessary layers. This approach allows for independent deployment and scaling of each feature, improving modularity and maintainability.

## NLayer Architecture

- **Presentation Layer**: Handles user interaction and presentation logic.
- **Business Logic Layer**: Contains business rules and domain logic.
- **Data Access Layer**: Manages data persistence and retrieval.


-------

## Project Structure 

The **DotNet8.BankingManagementSystem** project is structured to implement a banking management system using **.NET 8**. Here's an overview of the project structure based on the repository's content:

- **Backend Services**: This layer contains the business logic and service layer for the application. It references the Database, Mapper, Models, and Shared projects to perform its operations.
- **Common**: This project includes common elements that are shared across different layers of the application, such as global settings and utilities.
- **Database**: This project manages the database operations, likely including entity definitions and the data access layer.
- **Frontend**: While not detailed in the search results, this would typically contain the user interface components of the application.
- **Mapper**: This project is responsible for mapping between different data models, which is a common practice in applications to separate the internal data representation from the external one.
- **Models**: This project defines the data models used throughout the application.
- **Shared**: This project likely contains shared resources and components that can be used by both the frontend and backend.

The project seems to follow a **Vertical Slice Architecture** combined with an **NLayer Architecture**, which means that each feature or slice of the application is developed independently across all the layers from the database to the user interface. This approach allows for better modularity and separation of concerns.
