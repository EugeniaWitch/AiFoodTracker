using System.ComponentModel.DataAnnotations;
using server.Models.Enums;

namespace server.DTOs.Products;

public class CreateProductRequest
{
    [Required(ErrorMessage ="Name is required")]
    [StringLength(100,MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name{get;set;} = string.Empty;

    [Required(ErrorMessage ="Brand is required")]
    [StringLength(100,MinimumLength =2, ErrorMessage ="Brand must be between 2 and 100 characters")]
    public string? Brand{get;set;}

    [Required(ErrorMessage ="Type is required")]
    public ProductType? Type{get;set;}

    [Required(ErrorMessage ="CategoryId is required")]
    public Guid CategoryId{get;set;}

    public ProductVisibility Visibility{get;set;} = ProductVisibility.Private;

    [Range(0,10000, ErrorMessage ="Calories must be greater than or equal to 0")]
    public double Calories{get;set;}

    [Range(0,10000,ErrorMessage ="Protein must be greater than or equal to 0")]
    public double Protein{get;set;}

    [Range(0,10000,ErrorMessage ="Fat must be greater than or equal to 0")]
    public double Fat{get;set;}

    [Range(0,10000,ErrorMessage ="Carbs must be greater than or equal to 0")]
    public double Carbs{get;set;}

    [Range(0,10000,ErrorMessage ="Sugar must be greater than or equal to 0")]
    public double? Sugar{get;set;}

    [Range(0,10000,ErrorMessage ="Fiber must be greater than or equal to 0")]
    public double? Fiber{get;set;}

    [Range(0,10000,ErrorMessage ="Iron must be greater than or equal to 0")]
    public double? IronMg{get;set;}

    [Required(ErrorMessage ="DefaultUnit is required")]
    public ProductUnit? DefaultUnit{get;set;}

    [Required(ErrorMessage ="NutritionUnit is required")]
    public ProductUnit? NutritionUnit{get;set;}

    [Range(0.0001,100000,ErrorMessage ="NutritionAmount must be greater than 0")]
    public double NutritionAmount{get;set;}

    public double? ServingSize{get;set;}
    public ProductUnit? ServingSizeUnit{get;set;}

    [StringLength(100,ErrorMessage ="ServingDescription must be less than 100 characters")]
    public string? ServingDescription{get;set;}
}