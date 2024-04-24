
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _context;

    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(Product Product)
    {
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();
        return Product;
    }

    public async Task UpdateAsync(Product Product)
    {
        _context.Entry(Product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Product = await _context.Products.FindAsync(id);
        if (Product != null)
        {
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
        }
    }
}