namespace server.Models;

public class UserSettings
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}

    public bool UseStorage{get;set;} = true;
    public bool UseCycleMode{get;set;} = false;

    public User User{get;set;} = null!;
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get;set;} = DateTime.UtcNow;
}