using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.Products;
using server.Models.Enums;

namespace server.Services.Products;

public class ProductCategoryService : IProductCategoryService
{
    private readonly AppDbContext _context;

    public ProductCategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategoryResponse>> GetCategoriesAsync(ProductType ? type)
    {
        var query = _context.ProductCategories.AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(category => category.Type == type.Value);
        }

        return await query.OrderBy(category => category.Type)
                            .ThenBy(category => category.Name)
                            .Select(category => new ProductCategoryResponse{
            Id = category.Id,
            Name = category.Name,
            Type = category.Type.ToString(),
            Icon = category.Icon,
        }).ToListAsync();
    }
}