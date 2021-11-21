using ShopBridge.DataAccess;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.UnitTests
{
    class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(InventoryContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Inventories.AddRange(
                new Inventory() { Name = "Bread", Description = "Food" },
                new Inventory() { Name = "Milk", Description = "Dairy Products" },
                new Inventory() { Name = "Eggs", Description = "Food" },
                new Inventory() { Name = "Rice", Description = "Food" }
            );       
            context.SaveChanges();
        }
    }
}
