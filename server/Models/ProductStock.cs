using server.Models.Enums;

namespace server.Models;

public class ProductStock
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}
    public User User{get;set;} = null!;

    public Guid ProductId{get;set;}
    public Product Product{get;set;} = null!;

    public double Quantity{get;set;}
    public ProductUnit Unit{get;set;}

    public DateOnly? ExpirationDate{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;

    public List<FoodEntry> FoodEntries{get;set;}=new();
    public List<DrinkEntry> DrinkEntries{get;set;}=new();
    
}