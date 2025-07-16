﻿using Microsoft.EntityFrameworkCore;
using ProductInventory.Models;

namespace ProductInventory.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
