# ShopBridge
ShopBridge is an Inventory Management system that allows user to manage and maintain his/her inventory with ease. It has been developed to allow users to add an inventory, delete an inventory, update an inventory and search list of inventories. 
It is built in .NET Core 3.1.
The solution consist following projects
1. ShopBridge (Asp.net web api core)
2. ShopBridge.Model (C# Model Layer)
3. ShopBridge.Service ( C# service layer)
4. ShopBridge.DataAccess ( C# Database layer)
5. ShopBridge.Utils( C# utils layer)
6. ShopBridge.UnitTests( XUnit Test Project)

# System Requirements
1. .NET Core 3.1
2. Visual Studio 2019 
3. SQL Server 2017
4. IIS should be installed

# Deployment
1. Make ShopBridge solution as a startup project.
2. Go to appsettings.json and update ShopBridgeConnection.
3. Right click on ShopBridge solution and click on Publish.
4. Select Folder as a Target and click Next
5. Provide local folder location and click on Finish.
6. Expand Show all settings
7. Expand Databases and check Use this connection string at runtime
7. Expand Entity Framework Migrations and check Use this connection string at runtime
9. Click on Save.
10. Click on Publish.
11. Now solution is publish on local folder location provided in step 5.
12. Go to published folder ->EFSQLSCRIPTS and run sql scipt using SSMS in configured db.
13. Create Images folder under published directory.
14. Run the service by double clicking on Shopbridge.exe

# Test Endpoints
Test endpoints using Postman tool.
1. GET- https://localhost:5001/api/inventory/{id}
2. GET- https://localhost:5001/api/inventory?Name=i&PageNumber=1&PageSize=10 (Query Parameters are optional)
3. POST- https://localhost:5001/api/inventory
Body: form-data
4. DELETE- https://localhost:5001/api/inventory/{id}
5. PUT- https://localhost:5001/api/inventory/{id}

# Run Tests
1. Go to ShopBridge.UnitTests -> InventoryUnitTestController and update connectionString with your local database connection string.
2. Click on Test -> Run all tests
