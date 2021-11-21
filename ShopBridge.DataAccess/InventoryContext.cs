using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.DataAccess
{
    public class InventoryContext : DbContext
    {

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }
        /*public InventoryContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=DESKTOP-LJ2O1A4;Database=ShopBridge;UID=db_user;PWD=Dogfood;");
        }*/
        public DbSet<Inventory> Inventories { get; set; }
    }
}
