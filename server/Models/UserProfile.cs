using server.Models.Enums;

namespace server.Models;

public class UserProfile
{
    public Guid Id{get;set;}
    public Guid UserId{get;set;}

    public double? HeightCm{get;set;}
    public DateOnly? BirthDate{get;set;}
    public UserSex Sex{get;set;} = UserSex.NotSpecified;
    public UserGoalType? GoalType{get;set;}

    public User User{get;set;}=null!;
    public DateTime CreatedAt{get;set;}
    public DateTime UpdatedAt{get;set;}
}