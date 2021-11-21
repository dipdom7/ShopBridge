using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Models;
using ShopBridge.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [ApiController]
    public class InventoryController : Controller
    {
        private IInventoryService _inventoryService;
        private IWebHostEnvironment _webHostEnvironment;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment webHostEnvironment)
        {
            this._inventoryService = inventoryService;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetInventories([FromQuery] InventoryParameters inventoryParameters)
        {
            try
            {
                var inventories = await _inventoryService.GetInventories(inventoryParameters);
                if(inventories == null)
                {
                    return NotFound();
                }
                return Ok(inventories);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetInventory(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            try
            {
                var inventory = await _inventoryService.GetInventory(id);
                if (inventory != null)
                {
                    return Ok(inventory);
                }
                return NotFound($"Inventory with id {id} is not found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
        [HttpPost]
        [Route("api/[controller]/")]
        public async Task<IActionResult> AddInventoryAsync([FromForm]Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(inventory.ImageFile != null)
                    {
                        inventory.ImageName = await SaveImage(inventory.ImageFile);
                    }
                   
                    await _inventoryService.AddInventory(inventory);
                    return Ok(inventory);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
                
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditInventory(int? id, Inventory inventory)
        {
            if(id == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var dbInventory = await _inventoryService.GetInventory(id);
                if (dbInventory != null)
                {
                    inventory.Id = (int)id;
                    await _inventoryService.EditInventory(inventory);
                    return Ok(inventory);
                }
                return NotFound($"Inventory with id {id} is not found.");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
           

        }
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteInventory(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            try
            {
                var dbInventory = await _inventoryService.GetInventory(id);
                if (dbInventory != null)
                {
                    await _inventoryService.DeleteInventory(dbInventory);
                    return Ok(dbInventory);
                }
                return NotFound($"Inventory with id {id} is not found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
            

        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var imagePath =  Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return imageName;
        }

    }
}
