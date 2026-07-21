using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
            .Where(product => product.Visibility == ProductVisibility.Public || product.OwnerId == userId)
            .AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(product => product.Type == type.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim().ToLower();
            query = query.Where(product => product.Name.ToLower().Contains(normalizedSearch));
        }

        var products = await query.OrderBy(product => product.Type)
            .ThenBy(product => product.Name).ToListAsync();

        return products.Select(MapToResponse).ToList();
    }

    public async Task<ProductResponse> CreateProductAsync(Guid userId, CreateProductRequest request)
    {
        var name = request.Name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Name is required");
        }

        var type = request.Type
            ?? throw new InvalidOperationException("Product type is required");

        var defaultUnit = request.DefaultUnit
            ?? throw new InvalidOperationException("Default unit is required");

        var nutritionUnit = request.NutritionUnit
            ?? throw new InvalidOperationException("Nutrition unit is required");

        ValidateUnits(type, defaultUnit, nutritionUnit);
        ValidateServingInfoIfNeeded(type, defaultUnit, nutritionUnit, request.ServingSize);

        var category = await _context.ProductCategories
            .FirstOrDefaultAsync(category => category.Id == request.CategoryId);

        if (category is null)
        {
            throw new InvalidOperationException("Category not found");
        }

        if (category.Type != type)
        {
            throw new InvalidOperationException("Category type does not match product type");
        }

        var now = DateTime.UtcNow;
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Brand = string.IsNullOrWhiteSpace(request.Brand) ? null : request.Brand.Trim(),
            Type = type,
            Visibility = request.Visibility,
            CategoryId = request.CategoryId,
            OwnerId = userId,
            NutritionAmount = request.NutritionAmount,
            NutritionUnit = nutritionUnit,
            Calories = request.Calories,
            Protein = request.Protein,
            Fat = request.Fat,
            Carbs = request.Carbs,
            Sugar = request.Sugar,
            Fiber = request.Fiber,
            IronMg = request.IronMg,
            DefaultUnit = defaultUnit,
            ServingSize = UsesPortion(defaultUnit, nutritionUnit) ? request.ServingSize : null,
            ServingDescription = UsesPortion(defaultUnit, nutritionUnit)? string.IsNullOrWhiteSpace(request.ServingDescription)
                    ? null : request.ServingDescription.Trim() : null,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        product.Category = category;

        return MapToResponse(product);
    }

    public async Task<ProductResponse> UpdateProductAsync(Guid userId, Guid productId, UpdateProductRequest request)
    {
        var existingProduct = await _context.Products.Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.Id == productId);

        if (existingProduct is null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        var name = request.Name.Trim();

         if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Name is required");
        }

        var type = request.Type ?? throw new InvalidOperationException("Product type is required");
        var defaultUnit = request.DefaultUnit ?? throw new InvalidOperationException("Default unir is required");
        var nutritionUnit = request.NutritionUnit ?? throw new InvalidOperationException("Nutrition unit is required");

        ValidateUnits(type, defaultUnit, nutritionUnit);
        ValidateServingInfoIfNeeded(type, defaultUnit, nutritionUnit, request.ServingSize);

        var category = await _context.ProductCategories.FirstOrDefaultAsync(category => category.Id == request.CategoryId);

        if (category is null)
        {
            throw new InvalidOperationException("Category not found");
        }

        if (category.Type != type)
        {
            throw new InvalidOperationException("Category type does not match product type");
        }

        var userOwnsProduct = existingProduct.OwnerId == userId;

        Product productToSave;

        if (userOwnsProduct)
        {
            productToSave = existingProduct;
        }
        else
        {
            productToSave = new Product
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Products.Add(productToSave);
        }

        productToSave.Name = name;
        productToSave.Brand = string.IsNullOrWhiteSpace(request.Brand) ? null : request.Brand.Trim();
        productToSave.Type = type;
        productToSave.Visibility = userOwnsProduct ? request.Visibility : ProductVisibility.Private;
        productToSave.CategoryId = request.CategoryId;
        productToSave.Category = category;
        productToSave.NutritionAmount = request.NutritionAmount;
        productToSave.NutritionUnit = nutritionUnit;
        productToSave.Calories = request.Calories;
        productToSave.Protein = request.Protein;
        productToSave.Fat = request.Fat;
        productToSave.Carbs = request.Carbs;
        productToSave.Sugar = request.Sugar;
        productToSave.Fiber = request.Fiber;
        productToSave.IronMg = request.IronMg;
        productToSave.DefaultUnit = defaultUnit;
        productToSave.ServingSize = UsesPortion(defaultUnit, nutritionUnit) ? 
            request.ServingSize : null;
        productToSave.ServingDescription = UsesPortion(defaultUnit, nutritionUnit) ? 
            string.IsNullOrWhiteSpace(request.ServingDescription) ? null : 
            request.ServingDescription.Trim() : null;
        productToSave.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
           
        return MapToResponse(productToSave);
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Brand=product.Brand,
            Type = product.Type.ToString(),
            Visibility = product.Visibility.ToString(),
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            OwnerId = product.OwnerId,
            NutritionAmount = product.NutritionAmount,
            NutritionUnit = product.NutritionUnit.ToString(),
            Calories = product.Calories,
            Protein = product.Protein,
            Fat = product.Fat,
            Carbs = product.Carbs,
            Sugar = product.Sugar,
            Fiber = product.Fiber,
            IronMg = product.IronMg,
            DefaultUnit = product.DefaultUnit.ToString(),
            ServingSize = product.ServingSize,
            ServingSizeUnit = product.ServingSize is null ? null : GetBaseUnit(product.Type).ToString(),
            ServingDescription = product.ServingDescription,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    private static void ValidateUnits(ProductType type, ProductUnit defaultUnit, ProductUnit nutritionUnit)
    {
        ValidateUnitForProductType(type, defaultUnit, "Default unit");
        ValidateUnitForProductType(type, nutritionUnit, "Nutrition unit");
    }

    private static void ValidateUnitForProductType(ProductType type, ProductUnit unit, string fieldName)
    {
        if (type == ProductType.Drink)
        {
            if (unit == ProductUnit.Gram)
            {
                throw new InvalidOperationException($"{fieldName} for drink cannot be Gram");
            }

            return;
        }

        if (unit == ProductUnit.Ml)
        {
            throw new InvalidOperationException($"{fieldName} for food cannot be Ml");
        }
    }

    private static void ValidateServingInfoIfNeeded(ProductType type, 
        ProductUnit defaultUnit,
        ProductUnit nutritionUnit,
        double? servingSize)
    {
        if (!UsesPortion(defaultUnit, nutritionUnit))
        {
            return;
        }

        if (servingSize is null || servingSize <= 0)
        {
            var baseUnit = GetBaseUnit(type);

            throw new InvalidOperationException(
                $"Serving size is required for portion products and must be in {baseUnit}"
            );
        }
    }

    private static bool UsesPortion(ProductUnit defaultUnit, ProductUnit nutritionUnit)
    {
        return defaultUnit == ProductUnit.Portion ||
            nutritionUnit == ProductUnit.Portion;
    }

    private static ProductUnit GetBaseUnit(ProductType type)
    {
        return type == ProductType.Drink
            ? ProductUnit.Ml
            : ProductUnit.Gram;
    }
}