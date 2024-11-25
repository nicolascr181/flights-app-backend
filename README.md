# flights-app-backend

## Architecture Review

### Overview
The backend server follows the **Clean Architecture** approach, ensuring separation of concerns, maintainability, and scalability. This architecture divides the system into distinct layers, allowing independent development, testing, and deployment of each component.

Target Framework: Net 8.0

### Key Features

1. **Clean Architecture**
   - The application is structured using the **Clean Architecture** pattern, ensuring a clear separation of concerns between different components. This approach helps in maintaining a well-organized and scalable backend system.

2. **Swagger Configuration**
   - The backend includes **Swagger** for API documentation, making it easy for developers and consumers to understand and interact with the API endpoints. Swagger is configured to provide an interactive UI for testing the endpoints.

3. **Logging**
   - Logs are stored in **txt** files, capturing important runtime information, errors, and events. This logging mechanism is critical for troubleshooting and monitoring the server's behavior in production.

4. **Database (SQLite)**
   - **SQLite** is used as the database to store **Journey** and **Flight** data. The database is populated when the server starts, using a pre-configured file called `markets.json`, which contains all of the available flights and prices.

5. **External API Integration**
   - The backend server integrates with an **external API** for currency conversion. This allows the system to fetch up-to-date exchange rates and perform necessary currency conversions as required. For more information, visit [Freecurrency Website Docs](https://freecurrencyapi.com/docs).

6. **Error Handling with ErrorOr Library**
   - Instead of traditional `try-catch` blocks, the **ErrorOr** library is used for error handling. This library provides a more structured way to handle and return errors, improving readability and maintainability.

7. **CQRS with MediatR**
   - The **CQRS (Command Query Responsibility Segregation)** pattern is employed, with **MediatR** used to handle commands and queries. This ensures that the system's read and write operations are efficiently separated, improving performance and scalability.

8. **Unit Testing with xUnit**
   - **xUnit** is configured for unit testing the backend logic, especially for ensuring correct flight route selection and currency conversion. This testing framework helps ensure that all core functionalities are working as expected.

---
This architecture provides a robust, maintainable, and scalable approach for the backend server, enabling efficient management of flights, journeys, and conversion processes.