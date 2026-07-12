using Microsoft.AspNetCore.Mvc;
using server.DTOs.Products;
using server.Models.Enums;
using server.Services.Products;

namespace server.Controllers;

[ApiController]
[Route("/api/product-categories")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryService _categoryService;

    public ProductCategoriesController(IProductCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductCategoryResponse>>> GetCategories([FromQuery] ProductType? type)
    {
        var categories = await _categoryService.GetCategoriesAsync(type);

        return Ok(categories);
    }
}