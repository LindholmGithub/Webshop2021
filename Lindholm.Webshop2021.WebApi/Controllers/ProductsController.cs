using System;
using System.Collections.Generic;
using System.Linq;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.WebApi.Dtos;
using Lindholm.Webshop2021.WebApi.Dtos.Products;
using Lindholm.Webshop2021.WebApi.PolicyHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lindholm.Webshop2021.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [Authorize(Policy=nameof(CanReadProductsHandler))]
        [HttpGet]
        public ActionResult<ProductsDto> ReadAll()
        {
            try
            {
                var products = _productService.GetAll()
                    .Select(p => new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        OwnerId = p.OwnerId
                        
                    })
                    .ToList();
                return Ok(new ProductsDto
                {
                    ProductsList = products,
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [Authorize(Policy=nameof(CanReadProductsHandler))]
        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            var dto = new ProductDto
            {
                Id = product.Id, 
                Name = product.Name, 
                OwnerId = product.OwnerId
            };
            return Ok(dto);
        }
        
        [Authorize(Policy = nameof(CanWriteProductsHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<ProductDto> DeleteProduct(int id)
        {
            var product = _productService.DeleteProduct(id);
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                OwnerId = product.OwnerId
            };
            return Ok(dto);
        }
        
        [Authorize(Policy = nameof(CanWriteProductsHandler))]
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductDto productDto)
        {
            var productToCreate = new Product()
            {
                Name = productDto.Name,
                OwnerId = productDto.OwnerId
            };
            var productCreated = _productService.CreateProduct(productToCreate);
            return Created($"https://localhost/api/Product/{productCreated.Id}",productCreated);
        }
        
        [Authorize(Policy = nameof(CanWriteProductsHandler))]
        [HttpPut("{id:int}")]
        public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] ProductDto productToUpdate)
        {
            if (id != productToUpdate.Id)
            {
                return BadRequest("Id in param must be the same as in object.");
            }
            return Ok(_productService.UpdateProduct(new Product()
            {
                Id = id,
                Name = productToUpdate.Name,
                OwnerId = productToUpdate.OwnerId
            }));
        }
    }
}