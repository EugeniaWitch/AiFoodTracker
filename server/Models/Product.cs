using server.Models.Enums;

namespace server.Models;

public class Product
{
    public Guid Id{get;set;}
    public string Name{get;set;}=string.Empty;
    public string? Brand{get;set;}
    public ProductType Type{get;set;}
    public ProductVisibility Visibility{get;set;} = ProductVisibility.Private;
    public ProductSourceType SourceType{get;set;}
    public ProductReviewStatus ReviewStatus{get;set;}=ProductReviewStatus.NotSubmitted;
    
    public Guid CategoryId{get;set;}
    public ProductCategory Category{get;set;} = null!;

    public Guid? OwnerId{get;set;}
    public User? Owner{get;set;}

    public double Calories{get;set;}
    public double Protein{get;set;}
    public double Fat{get;set;}
    public double Carbs{get;set;}
    public double? Sugar{get;set;}
    public double? Fiber{get;set;}
    public double? IronMg{get;set;}

    public ProductUnit DefaultUnit{get;set;}

    public double NutritionAmount{get;set;}
    public ProductUnit NutritionUnit{get;set;}

    public double? ServingSize{get;set;}
    public string? ServingDescription{get;set;}

    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;

    public List<ProductStock> ProductStocks{get;set;}=new();
    public List<FoodEntry> FoodEntries{get;set;}=new();
    public List<DrinkEntry> DrinkEntries{get;set;}=new();

}