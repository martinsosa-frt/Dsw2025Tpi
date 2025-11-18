using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Dtos;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsManagementService _service; 

        public ProductsController(ProductsManagementService service)
        {
            _service = service;
        }
        /*PARA OBTENER TODOS LOS PRODUCTOS*/
        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProducts();
            if (products == null || !products.Any()) return NoContent();
            return Ok(products);
        }

        /*PARA OBTENER UN PRODUCTO CON UN ID PARTICULAR*/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductBySku(Guid id)
        {
            var product = await _service.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        
        /*PARA CREAR UN NUEVO PRODUCTO*/
        [HttpPost()]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel.Request request)
        {
            try
            {
                var product = await _service.AddProduct(request);
                return Created($"product created/{product.Id}", product);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message); 
            }
            catch (DuplicatedEntityException de)
            {
                return Conflict(de.Message);
            }
            catch (Exception)
            {
                return Problem("Se produjo un error al guardar el producto");
            }
        }
        
        /*PARA MODIFICAR PROFUCTO POR ID*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductModel.UpdateRequest request)
        {
            try
            {
                var updatedProduct = await _service.UpdateProduct(id, request);
                return Ok(updatedProduct);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (EntityNotFoundException enfe)
            {
                return NotFound(enfe.Message);
            }
            catch (DuplicatedEntityException dee)
            {
                return Conflict(dee.Message);
            }
            catch (Exception)
            {
                return Problem("Error interno al actualizar el producto");
            }
        }

        /*INHABILITAR UN PRODUCTO*/
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProductPartial(Guid id, [FromBody] ProductModel.PatchRequest request)
        {   

            if (!request.IsActive.HasValue)
                return BadRequest("Debe proporcionar al menos un campo válido para actualizar");

            try
            {
                    var result = await _service.UpdateProductStatus(id, request.IsActive.Value);
                    return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Problem("Error al actualizar el producto");
            }
        }
    }
}
