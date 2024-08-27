using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;

        // Inicializar la base de datos con algunos productos si está vacía
        if (!_context.Products.Any())
        {
            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Producto 1", Price = 100 },
                new Product { Name = "Producto 2", Price = 200 },
                new Product { Name = "Producto 3", Price = 300 }
            });
            _context.SaveChanges();
        }
    }

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return _context.Products.ToList();
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Post([FromForm] Product product)
    {
        var image = HttpContext.Request.Form.Files.FirstOrDefault();

        if (image != null)
        {
            using var memoryStream = new System.IO.MemoryStream();
            await image.CopyToAsync(memoryStream);
            product.Image = memoryStream.ToArray();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromForm] Product updatedProduct)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Image = updatedProduct.Image;
        product.Quantity = updatedProduct.Quantity;
        var image = HttpContext.Request.Form.Files.FirstOrDefault();
        if (image != null)
        {
            using var memoryStream = new System.IO.MemoryStream();
            await image.CopyToAsync(memoryStream);
            product.Image = memoryStream.ToArray();
        }

        await _context.SaveChangesAsync();
        // string base64String = Convert.ToBase64String(product.Image);
        // return CreatedAtAction(nameof(Get), new { id = product.Id }, base64String);
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}