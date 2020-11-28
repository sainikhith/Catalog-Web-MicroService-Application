using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/nik/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Private Fields
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        #endregion

        #region Constructor
        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region GetList
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }
        #endregion

        #region GetProductById
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if(product == null)
            {
                _logger.LogError($"Product with id: {id} ,not found.");
                return NotFound();
            }
            return Ok(product);
        }
        #endregion

        #region GetCategoryByName
        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = await _productRepository.GetProductByCategory(category);
            if (product == null)
            {
                _logger.LogError($"Product with Category Name: {category} ,not found.");
                return NotFound();
            }
            return Ok(product);
        }
        #endregion

        #region Create Product
        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        #endregion

        #region Update Product
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }
        #endregion

        #region Delete Product
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
        #endregion


    }
}
