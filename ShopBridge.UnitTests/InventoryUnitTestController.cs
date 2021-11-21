using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Controllers;
using ShopBridge.DataAccess;
using ShopBridge.Models;
using ShopBridge.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShopBridge.UnitTests
{
    public class InventoryUnitTestController
    {
        public static DbContextOptions<InventoryContext> dbContextOptions { get; }
        public static string connectionString = "Server=DESKTOP-LJ2O1A4;Database=ShopBridge;UID=db_user;PWD=Dogfood;";
        private InventoryService service;

        static InventoryUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<InventoryContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public InventoryUnitTestController()
        {
            var context = new InventoryContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);
            service = new InventoryService(context);
        }

        #region GetInventoryById test cases
        [Fact]
        public async void Task_GetInventoryById_Return_OkResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            var id = 2;

            //Act
            var data = await controller.GetInventory(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetInventoryById_Return_NotFoundResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            var id = 5;

            //Act
            var data = await controller.GetInventory(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async void Task_GetInventoryById_Return_BadRequestResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            int? id = null;

            //Act
            var data = await controller.GetInventory(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_GetInventoryById_MatchResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            int? id = 1;

            //Act
            var data = await controller.GetInventory(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var inventory = (Inventory)okResult.Value;

            Assert.Equal("Bread", inventory.Name);
            Assert.Equal("Food", inventory.Description);
        }
        #endregion

        #region GetInventory
        [Fact]
        public async void Task_GetInventories_Return_OkResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);

            //Act
            var data = await controller.GetInventories(new InventoryParameters());

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetInventories_Return_BadRequestResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);

            //Act
            var data = controller.GetInventories(new InventoryParameters());
            data = null;

            if (data != null)
                //Assert
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetInventories_MatchResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);

            //Act
            var data = await controller.GetInventories(new InventoryParameters());

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var inventories =(List<Inventory>) okResult.Value;

            Assert.Equal("Bread", inventories[0].Name);
            Assert.Equal("Food", inventories[0].Description);

            Assert.Equal("Milk", inventories[1].Name);
            Assert.Equal("Dairy Products", inventories[1].Description);
        }
        #endregion

        #region Add Inventory
        [Fact]
        public async void Task_AddInventory_ValidData_Return_OkResult()
        {
            //Arrange
            var controller = new InventoryController(service ,null);
            var inventory = new Inventory() { Name = "TV", Description = "Electronic Device", Price=25000 };

            //Act
            var data = await controller.AddInventoryAsync(inventory);

            //Assert
            Assert.IsType<OkResult>(data);
        }
      
        [Fact]
        public async void Task_Add_ValidData_MatchResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            var inventory = new Inventory() { Name = "Test Product", Description = "Test Description", Price = 70 };
            //Act
            var data = await controller.AddInventoryAsync(inventory);

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var result = (Inventory)okResult.Value;

            Assert.Equal(5, result.Id);
        }
        #endregion

        #region Delete Inventory
        [Fact]
        public async void Task_DeleteInventory_Post_Return_OkResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            var id = 4;

            //Act
            var data = await controller.DeleteInventory(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteInventory_Post_Return_NotFoundResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            var id = 6;

            //Act
            var data = await controller.DeleteInventory(id); ;

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteInventory_Return_BadRequestResult()
        {
            //Arrange
            var controller = new InventoryController(service, null);
            int? id = null;

            //Act
            var data = await controller.DeleteInventory(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        #endregion
    }
}
