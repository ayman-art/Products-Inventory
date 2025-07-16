using Microsoft.EntityFrameworkCore;
using ProductInventory.Data;
using ProductInventory.Models;

namespace ProductInventory.Services
{

    public class ProductService : IProductService
    {
        private readonly InventoryContext _context;

        public ProductService(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            // Create a stub entity with just the ID
            var existing = new Product { Id = id };

            // Attach it to the context (not loaded from DB)
            _context.Products.Attach(existing);

            
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Category = product.Category;
            existing.Quantity = product.Quantity;

            try
            {
                // This will generate an UPDATE statement only for the modified fields
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0; 
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine("PRODUCT NOT EXISTING.");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetInStockAsync() =>
            await _context.Products.Where(p => p.Quantity > 0).ToListAsync();
    }
}
