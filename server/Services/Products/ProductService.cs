using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.Products;
using server.Models;
using server.Models.Enums;

namespace server.Services.Products;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponse>> GetProductsAsync(Guid userId, ProductType? type, string? search)
    {
        var query = _context.Products.Include(product => product.Category)
        .Where(product => product.Visibility == ProductVisibility.Public || product.OwnerId == userId).AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(product => product.Type == type.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim().ToLower();
            query = query.Where(product => product.Name.ToLower().Contains(normalizedSearch));
        }

        return await query.OrderBy(product => product.Type)
        .ThenBy(product => product.Name).Select(product => MapToResponse(product)).ToListAsync();
    }

    public async Task<ProductResponse> CreateProductAsync(Guid userId, CreateProductRequest request)
    {
        var name = request.Name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Name is required");
        }
        if (!Enum.IsDefined(typeof(ProductType), request.Type))
        {
            throw new InvalidOperationException("Invalid product type");
        }
        if (!Enum.IsDefined(typeof(ProductVisibility), request.Visibility))
        {
            throw new InvalidOperationException("Invalid product visibility");
        }
        if (!Enum.IsDefined(typeof(ProductUnit), request.DefaultUnit))
        {
            throw new InvalidOperationException("Invalid product unit");
        }

        var category = await _context.ProductCategories.FirstOrDefaultAsync(category => category.Id == request.CategoryId);

        if (category is null)
        {
            throw new InvalidOperationException("Category not found");
        }
        if (category.Type != request.Type)
        {
            throw new InvalidOperationException("Category type does not match product type");
        }

        var now = DateTime.UtcNow;
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = request.Type,
            Visibility = request.Visibility,
            CategoryId = request.CategoryId,
            OwnerId = userId,
            CaloriesPer100 = request.CaloriesPer100,
            ProteinPer100 = request.ProteinPer100,
            FatPer100 = request.FatPer100,
            CarbsPer100 = request.CarbsPer100,
            SugarPer100 = request.SugarPer100,
            FiberPer100 = request.FiberPer100,
            IronMgPer100 = request.IronMgPer100,
            DefaultUnit = request.DefaultUnit,
            CreatedAt = now,
            UpdatedAt = now,
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        product.Category = category;

        return MapToResponse(product);
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Type = product.Type.ToString(),
            Visibility = product.Visibility.ToString(),
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            OwnerId = product.OwnerId,
            CaloriesPer100 = product.CaloriesPer100,
            ProteinPer100 = product.ProteinPer100,
            FatPer100 = product.FatPer100,
            CarbsPer100 = product.CarbsPer100,
            SugarPer100 = product.SugarPer100,
            FiberPer100 = product.FiberPer100,
            IronMgPer100 = product.IronMgPer100,
            DefaultUnit = product.DefaultUnit.ToString(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        };
    }
}