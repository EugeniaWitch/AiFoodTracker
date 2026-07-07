namespace server.Models;

public class NutritionGoal
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}

    public double? Calories{get;set;}
    public double? Protein{get;set;}
    public double? Fat{get;set;}
    public double? Carbs{get;set;}
    public double? Sugar{get;set;}
    public double? Fiber{get;set;}
    public double? WaterMl{get;set;}
    public double? IronMg{get;set;}

    public User User{get;set;}=null!;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;
}