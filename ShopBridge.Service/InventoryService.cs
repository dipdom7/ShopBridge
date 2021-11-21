﻿using Microsoft.EntityFrameworkCore;
using ShoBridge.Utils;
using ShopBridge.DataAccess;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Service
{
    public class InventoryService : IInventoryService
    {
        private InventoryContext _context;

        public InventoryService(InventoryContext inventoryContext)
        {
            _context = inventoryContext;
        }

        /// <summary>
        /// Add Product to Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns>return Added inventory</returns>
        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        /// <summary>
        /// Delete Product from Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public async Task DeleteInventory(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Product from Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns>Inventory</returns>
        public async Task<Inventory> EditInventory(Inventory dbInventory)
        {
            _context.Entry(dbInventory).State = EntityState.Modified;
            //_context.Entry(InventoryMaster).State = EntityState.Modified;
            _context.Inventories.Update(dbInventory);
            await _context.SaveChangesAsync();
            return dbInventory;
        }

        /// <summary>
        ///Get Products from Inventory
        /// </summary>
        /// <param name="inventoryParameters"></param>
        /// <returns>List of Inventory</returns>
        public async Task<List<Inventory>> GetInventories(InventoryParameters inventoryParameters)
        {
            var inventoryList = await _context.Inventories.ToListAsync();

            if (inventoryParameters.Name != null)
            {
                SearchByName(ref inventoryList, inventoryParameters.Name);
            }
            return PagedList<Inventory>.ToPagedList(inventoryList, inventoryParameters.PageNumber, inventoryParameters.PageSize);
        }

        /// <summary>
        ///Get Products from Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Inventory</returns>
        public async Task<Inventory> GetInventory(int? id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            return inventory;
        }

        private void SearchByName(ref List<Inventory> inventories, string name)
        {
            if (!inventories.Any() || string.IsNullOrWhiteSpace(name))
                return;
            inventories = inventories.Where(o => o.Name.ToLower().Contains(name.Trim().ToLower())).ToList();
        }
    }
}
