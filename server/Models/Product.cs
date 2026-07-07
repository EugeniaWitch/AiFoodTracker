using server.Models.Enums;

namespace server.Models;

public class Product
{
    public Guid Id{get;set;}
    public string Name{get;set;}=string.Empty;
    public ProductType Type{get;set;}
    public ProductVisibility Visibility{get;set;} = ProductVisibility.Public;
    
    public Guid CategoryId{get;set;}
    public ProductCategory Category{get;set;} = null!;

    public Guid? OwnerId{get;set;}
    public User? Owner{get;set;}

    public double CaloriesPer100{get;set;}
    public double ProteinPer100{get;set;}
    public double FatPer100{get;set;}
    public double CarbsPer100{get;set;}
    public double? SugarPer100{get;set;}
    public double? FiberPer100{get;set;}
    public double? IronMgPer100{get;set;}

    public ProductUnit DefaultUnit{get;set;}

    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;

    public List<ProductStock> ProductStocks{get;set;}=new();
    public List<FoodEntry> FoodEntries{get;set;}=new();
    public List<DrinkEntry> DrinkEntries{get;set;}=new();

}