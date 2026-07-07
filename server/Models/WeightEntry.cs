namespace server.Models;

public class WeightEntry
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}

    public DateOnly Date{get;set;}
    public double WeightKg{get;set;}

    public User User{get;set;}=null!;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
}