using Microsoft.AspNetCore.Mvc;
using ProductInventory.Models;
using ProductInventory.Services;

namespace ProductInventory.Controllers
{
    [ApiController]
    [Route("api/products/")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            var success = await _service.UpdateAsync(id, product);
            return success ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpGet("in-stock")]
        public async Task<IActionResult> GetInStock() =>
            Ok(await _service.GetInStockAsync());

    }
}
