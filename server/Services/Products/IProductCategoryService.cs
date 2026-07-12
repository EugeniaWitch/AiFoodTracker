using server.DTOs.Products;
using server.Models.Enums;

namespace server.Services.Products;

public interface IProductCategoryService
{
    Task<List<ProductCategoryResponse>> GetCategoriesAsync(ProductType?type);
}