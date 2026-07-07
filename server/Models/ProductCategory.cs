using server.Models.Enums;

namespace server.Models;

public class ProductCategory
{
    public Guid Id{get;set;}
    public string Name{get;set;} = string.Empty;
    public ProductType Type{get;set;}
    public string? Icon{get;set;}
    public List<Product> Products{get;set;} = new();
}