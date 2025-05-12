# Integratre_MockAPI_Application

This project is a simple RESTful Web API built using ASP.NET Core 8.0, which integrates with the mock API provided at https://restful-api.dev. The application exposes methods to interact with the mock API by performing CRUD operations (Create, Read, Update, Delete) on the mock data. This API also includes features such as validation, error handling, and pagination.

Features
- Retrieve Objects: Fetches data from the mock API with the ability to filter by name and supports pagination.

- Create Objects: Allows creating new objects with specific attributes and returns the created object.

- Update Objects: Supports updating an object by either its ID or name.

- Delete Objects: Allows the deletion of objects by ID.

Prerequisites
- .NET 8.0 SDK

- Visual Studio or VSCode

- Git for version control

Installation
Follow these steps to get the project up and running locally:

1. Clone the repository:
git clone https://github.com/sainikhilgb/Integratre_MockAPI_Application.git

2. Navigate to the project directory:
   cd Integrate-MockAPI
3. Restore dependencies:
   dotnet restore
4. Build the project:
   dotnet build
5. Run the project:
   dotnet run
   
# Swagger link for allAPI's 
http://localhost:5062/swagger/v1/swagger.json

# API Documentation via Swagger
Swagger UI is enabled by default. After running the project, navigate to:
   https://localhost:5097/swagger/index.html
There, you can:
- View all API endpoints
- Try out GET, POST, PUT, DELETE operations
- See request/response formats
  
# Adjustments for Your Project
- ID as GUID: Since the mock API stores the id as a GUID, you must ensure to use the id returned from the POST response for the PUT and DELETE operations. You cannot modify
  the existing data by modifying the id manually.
- Verify with POST: Before performing any PUT or DELETE operations, first create a new record using the POST request, then use the id from the response to verify the PUT and
  DELETE operations.
# Validation and Error Handling
- All input models are validated to ensure the required fields are provided and are valid.

- If any errors occur during the API calls, appropriate error messages are returned with status codes (e.g., 400 for bad requests, 500 for internal server errors).

- If any invalid data is returned from the mock API, it will be handled gracefully.
 
