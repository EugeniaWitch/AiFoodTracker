using server.DTOs.Products;
using server.Models.Enums;

namespace server.Services.Products;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync(Guid userId, ProductType? type, string? search);
    Task<ProductResponse> CreateProductAsync(Guid userId, CreateProductRequest request);
    Task<ProductResponse> UpdateProductAsync(Guid userId, Guid productId, UpdateProductRequest request);
}