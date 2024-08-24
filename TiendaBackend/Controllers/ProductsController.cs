using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
    public ActionResult<Product> Post(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, Product updatedProduct)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}