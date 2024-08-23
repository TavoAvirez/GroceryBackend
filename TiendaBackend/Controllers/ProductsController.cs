using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TiendaBackend.Controllers
{
    /// <summary>
    /// Controlador para gestionar productos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;

        public ProductsController()
        {
            // Inicializar la lista de productos
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Producto 1", Price = 10.99 },
                new Product { Id = 2, Name = "Producto 2", Price = 19.99 },
                new Product { Id = 3, Name = "Producto 3", Price = 7.99 }
            };
        }

        // GET: api/products
        /// <summary>
        /// Obtiene la lista de productos
        /// </summary>
        /// <returns>lista de productos</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _products;
        }

        // GET: api/products/{id}
        /// <summary>
        /// Obtiene un producto por su id
        /// </summary>
        /// <param name="id">el id del producto</param>
        /// <returns>el producto que coincida con el id</returns>
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.Find(p => p.Id == id);
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
            _products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Product updatedProduct)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return NoContent();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}