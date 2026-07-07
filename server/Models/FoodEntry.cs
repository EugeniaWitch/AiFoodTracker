using server.Models.Enums;

namespace server.Models;

public class FoodEntry
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}
    public User User{get;set;} = null!;

    public Guid ProductId{get;set;}
    public Product Product{get;set;} = null!;
    public Guid? ProductStockId { get; set; }
    public ProductStock? ProductStock { get; set; }

    public DateOnly Date{get;set;}
    public MealType MealType{get;set;}

    public double Amount{get;set;}
    public ProductUnit Unit{get;set;}

    public double Calories{get;set;}
    public double Protein{get;set;}
    public double Fat{get;set;}
    public double Carbs{get;set;}
    public double? Sugar{get;set;}
    public double? Fiber{get;set;}
    public double? IronMg{get;set;}

    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;
}