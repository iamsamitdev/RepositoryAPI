using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _ProductRepository;

    public ProductController(IProductRepository ProductRepository)
    {
        _ProductRepository = ProductRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _ProductRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var Product = await _ProductRepository.GetByIdAsync(id);
        if (Product == null)
            return NotFound();
        return Ok(Product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product Product)
    {
        var createdProduct = await _ProductRepository.CreateAsync(Product);
        return CreatedAtAction(
            nameof(Get), new { id = createdProduct.Id }, createdProduct
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product Product)
    {
        if (id != Product.Id)
            return BadRequest();

        await _ProductRepository.UpdateAsync(Product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ProductRepository.DeleteAsync(id);
        return NoContent();
    }
}