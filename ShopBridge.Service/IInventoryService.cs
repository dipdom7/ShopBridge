using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Service
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetInventories(InventoryParameters inventoryParameters);

        Task<Inventory> GetInventory(int? id);

        Task<Inventory> AddInventory(Inventory inventory);

        Task DeleteInventory(Inventory inventory);

        Task<Inventory> EditInventory(Inventory inventory);
    }
}
