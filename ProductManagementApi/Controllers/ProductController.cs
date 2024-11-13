using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Data;
using ProductManagementApi.Dtos;
using ProductManagementApi.Extensions;
using ProductManagementApi.Helpers;
using ProductManagementApi.Interfaces;
using ProductManagementApi.Mappers;
using System.Runtime.CompilerServices;

namespace ProductManagementApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(ApplicationDBContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var products = await _productRepository.GetAllAsync(query);
            var productsDto = products.Select(p => p.ToProductDto()).ToList();
            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product.ToProductDto());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = createProductDto.ToProductFromCreateDto();
            string username = User.GetUsername();
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri($"http://usermanagementapi:8081/api/account/getuserid{username}");
                var response = await client.SendAsync(request);
                result = await response.Content.ReadAsStringAsync();
            }
            productModel.UserId = result;
            await _productRepository.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [Authorize]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string username = User.GetUsername();
            string userId;
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri($"http://usermanagementapi:8081/api/account/getuserid{username}");
                var response = await client.SendAsync(request);
                userId = await response.Content.ReadAsStringAsync();
            }
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            if (product.UserId == userId)
            {
                var productModel = await _productRepository.UpdateAsync(id, updateProductDto);
                return Ok(productModel.ToProductDto());
            }
            else
            {
                return Unauthorized("You can only modify products you created");
            }

        }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string username = User.GetUsername();
            string userId;
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri($"http://usermanagementapi:8081/api/account/getuserid{username}");
                var response = await client.SendAsync(request);
                userId = await response.Content.ReadAsStringAsync();
            }
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (product.UserId == userId)
            {
                var productModel = await _productRepository.DeleteAsync(id);
                return NoContent();
            }
            else
            {
                return Unauthorized("You can only delete products you created");
            }

        }
    }
}
