using System;
using System.Collections.Generic;
using System.Linq;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.WebApi.Dtos;
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
        [HttpGet]
        public ActionResult<ProductsDto> ReadAll()
        {
            try
            {
                var products = _productService.GetAll()
                    .Select(p => new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name
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

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            var dto = new ProductDto {Id = product.Id, Name = product.Name};
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductDto> DeleteProduct(int id)
        {
            var product = _productService.DeleteProduct(id);
            var dto = new ProductDto {Id = product.Id, Name = product.Name};
            return Ok(dto);
        }

    }
}