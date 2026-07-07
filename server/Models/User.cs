using server.Models.Enums;

namespace server.Models;

public class User
{
    public Guid Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public UserRole Role{get; set;} = UserRole.User;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;
    public DateTime? PasswordChangedAt {get;set;}

    public UserProfile? Profile{get;set;}
    public UserSettings? Settings{get;set;}
    public NutritionGoal? NutritionGoal{get;set;}

    public List<WeightEntry> WeightEntries{get;set;}=new();
    public List<Product> Products{get;set;}=new();
    public List<ProductStock> ProductStocks{get;set;}=new();
    public List<FoodEntry> FoodEntries{get;set;}=new();
    public List<DrinkEntry> DrinkEntries{get;set;}=new();
}