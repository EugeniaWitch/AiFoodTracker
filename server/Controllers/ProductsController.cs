using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.DTOs.Products;
using server.Models.Enums;
using server.Services.Products;

namespace server.Controllers;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts([FromQuery] ProductType? type, [FromQuery] string? search)
    {
        var userId = GetCurrentUserId();
        var products = await _productService.GetProductsAsync(userId, type, search);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            var product = await _productService.CreateProductAsync(userId, request);
            return Created($"/api/products/{product.Id}", product);
        } catch (InvalidOperationException exception)
        {
            return BadRequest(new {message = exception.Message});
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();

            var product = await _productService.UpdateProductAsync(
                userId,
                id,
                request
            );

            return Ok(product);
        } catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message});
        } catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message});
        }
    }

    private Guid GetCurrentUserId()
    {
        var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user id");
        }

        return userId;
    }
}