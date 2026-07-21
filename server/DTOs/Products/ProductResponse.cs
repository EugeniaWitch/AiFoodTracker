namespace server.DTOs.Products;

public class ProductResponse
{
    public Guid Id{get;set;}
    public string Name{get;set;} = string.Empty;
    public string? Brand{get;set;}
    public string Type{get;set;} =string.Empty;
    public string Visibility{get;set;} =string.Empty;
    public Guid CategoryId{get;set;}
    public string CategoryName{get;set;}=string.Empty;
    public Guid? OwnerId{get;set;}
    public double Calories{get;set;}
    public double Protein{get;set;}
    public double Fat{get;set;}
    public double Carbs{get;set;}
    public double? Sugar{get;set;}
    public double? Fiber{get;set;}
    public double? IronMg{get;set;}
    public string DefaultUnit{get;set;}=string.Empty;
    public double NutritionAmount{get;set;}
    public string NutritionUnit{get;set;}=string.Empty;
    public double? ServingSize{get;set;}
    public string? ServingSizeUnit{get;set;}=string.Empty;
    public string? ServingDescription{get;set;}=string.Empty;
    public DateTime CreatedAt{get;set;}
    public DateTime UpdatedAt{get;set;}
}