using DemoQRCode.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoQRCode.Services
{
    public class ProductService:IProductService
    {
        private readonly DatabaseContext _dbContext;
        public ProductService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            Product product = await _dbContext.Products.FindAsync(productId); 
            if (product != null) { 
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}
