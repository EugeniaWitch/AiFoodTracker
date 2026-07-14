namespace server.DTOs.Products;

public class ProductResponse
{
    public Guid Id{get;set;}
    public string Name{get;set;} = string.Empty;
    public string Type{get;set;} =string.Empty;
    public string Visibility{get;set;} =string.Empty;
    public Guid CategoryId{get;set;}
    public string CategoryName{get;set;}=string.Empty;
    public Guid? OwnerId{get;set;}
    public double CaloriesPer100{get;set;}
    public double ProteinPer100{get;set;}
    public double FatPer100{get;set;}
    public double CarbsPer100{get;set;}
    public double? SugarPer100{get;set;}
    public double? FiberPer100{get;set;}
    public double? IronMgPer100{get;set;}
    public string DefaultUnit{get;set;}=string.Empty;
    public DateTime CreatedAt{get;set;}
    public DateTime UpdatedAt{get;set;}
}