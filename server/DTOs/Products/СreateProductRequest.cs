using System.ComponentModel.DataAnnotations;
using server.Models.Enums;

namespace server.DTOs.Products;

public class CreateProductRequest
{
    [Required(ErrorMessage ="Name is required")]
    [StringLength(100,MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name{get;set;} = string.Empty;

    [Required(ErrorMessage ="Type is required")]
    public ProductType Type{get;set;}

    [Required(ErrorMessage ="CategoryId is required")]
    public Guid CategoryId{get;set;}

    public ProductVisibility Visibility{get;set;} = ProductVisibility.Private;

    [Range(0,10000, ErrorMessage ="Calories must be greater than or equal to 0")]
    public double CaloriesPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Protein must be greater than or equal to 0")]
    public double ProteinPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Fat must be greater than or equal to 0")]
    public double FatPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Carbs must be greater than or equal to 0")]
    public double CarbsPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Sugar must be greater than or equal to 0")]
    public double SugarPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Fiber must be greater than or equal to 0")]
    public double FiberPer100{get;set;}

    [Range(0,10000,ErrorMessage ="Iron must be greater than or equal to 0")]
    public double IronMgPer100{get;set;}

    [Required(ErrorMessage ="DefaultUnit is required")]
    public ProductUnit DefaultUnit{get;set;}
}