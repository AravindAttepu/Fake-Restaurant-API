# Fake Restaurant API

A simple RESTful API built using ASP.NET Core and Entity Framework Core to simulate restaurant data. This API provides endpoints for retrieving restaurant details, menus, and orders. It is designed for testing and development purposes, offering mock data to simulate real-world scenarios.

**Features**

  - **Restaurant Details:** Retrieve information about the restaurant.
  - **Menu:** Fetch the restaurant's menu with item names, descriptions, and prices.
  - **Orders:** Place and view orders at the restaurant.
  - **Order History:** View past orders made by customers.
  - **Authentication:** Simulate mock authentication (basic).
    
**Endpoints**

Order Endpoints

    POST /api/Order/{restaurantid}/makeorder
    Description: Place a new order for a specific restaurant.

    GET /api/Order
    Description: Get all orders.

    DELETE /api/Order/{Order_id}
    Description: Delete a specific order by its Order_id.

    DELETE /api/Order/master/{master_id}
    Description: Delete orders based on a master_id.

    GET /api/Order/{id}
    Description: Retrieve details of a specific order by its id.

Restaurant Endpoints

    GET /api/Restaurant
    Description: Get a list of all restaurants.

    POST /api/Restaurant
    Description: Create a new restaurant.

    GET /api/Restaurant/{Restaurant_id}
    Description: Get details of a specific restaurant by its Restaurant_id.

    GET /api/Restaurant/{Restaurant_id}/menu
    Description: Get the menu of a specific restaurant by its Restaurant_id.

    POST /api/Restaurant/{Restaurant_id}/additem
    Description: Add a new menu item to a specific restaurant.

    GET /api/Restaurant/items
    Description: Get a list of all items across all restaurants.

User Endpoints

    POST /api/User/register
    Description: Register a new user.

    GET /api/User/getusercode
    Description: Get the user code for a user.

    GET /api/User
    Description: Get a list of all users.

    DELETE /api/User/{apikey}
    Description: Delete a user by their apikey.

    PUT /api/User/{apikey}
    Description: Update user details by their apikey.

**Technologies Used**

  - ASP.NET Core: Framework for building the REST API.
  - Entity Framework Core (EF Core): ORM used to interact with the database.
  - JSON: Data format for API responses.
  - .NET: The platform for building and running the application (.NET 8.0)


> This project was created to contribute to my knowledge and experience with .NET Web API. It showcases the use of ASP.NET Core and Entity Framework Core to build a simple RESTful API. The goal is to enhance my skills in web development and share a practical implementation of building APIs with .NET.

**Contributing**

  - Fork the repository.
  - Create a feature branch (`git checkout -b feature-xyz`).
  - Commit your changes (`git commit -am 'Add feature xyz'`).
  - Push to the branch (`git push origin feature-xyz`).
  - Open a pull request.
