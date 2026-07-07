namespace server.Models;

public class DrinkEntry
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}
    public User User{get;set;} = null!;

    public Guid ProductId{get;set;}
    public Product Product{get;set;}= null!;

    public Guid? ProductStockId{get;set;}
    public ProductStock? ProductStock{get;set;}
    
    public DateOnly Date{get;set;}
    public double AmountMl{get;set;}

    public double? Calories{get;set;}
    public double? Sugar{get;set;}
    public double? CaffeineMg{get;set;}

    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;
}