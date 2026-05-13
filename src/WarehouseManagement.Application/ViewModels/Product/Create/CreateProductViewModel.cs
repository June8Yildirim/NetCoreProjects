using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// This ViewModel is a "blueprint" for the Create Product form.
/// It contains all the fields we want to capture when adding a new 
/// product to our catalog, along with data-validation rules.
/// </summary>
namespace WarehouseManagement.Application.ViewModels.Product.Create
{
  public class CreateProductViewModel
  {
    /// <summary>
    /// The unique ID of the product. Only populated during Edit operations.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// The display name of the product (e.g. "Standard Crate").
    /// </summary>
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique Stock Keeping Unit identifier.
    /// </summary>
    [Required]
    [Display(Name = "SKU")]
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// The stock level at which a new order should be triggered.
    /// </summary>
    [Required]
    [Display(Name = "Reorder Level")]
    public int ReorderLevel { get; set; }

    /// <summary>
    /// Optional: The cost per individual unit.
    /// </summary>
    [Display(Name = "Unit Cost")]
    public decimal? UnitCost { get; set; }

    /// <summary>
    /// Optional: How much the product weighs.
    /// </summary>
    [Display(Name = "Weight (Lbs)")]
    public decimal? WeightLbs { get; set; }

    /// <summary>
    /// Optional: To help group products (e.g. "Packaging", "Raw Materials").
    /// </summary>
    [Display(Name = "Category")]
    public string? Category { get; set; }

    /// <summary>
    /// Optional: The physical barcode string.
    /// </summary>
    [Display(Name = "Barcode")]
    public string? Barcode { get; set; }

    /// <summary>
    /// The ID of the Supplier who provides this product.
    /// </summary>
    [Required]
    [Display(Name = "Supplier")]
    public Guid SupplierId { get; set; }

    /// <summary>
    /// A list of Suppliers to choose from in the UI dropdown.
    /// This is filled by the Service before the page loads.
    /// </summary>
    public IEnumerable<SelectListItem>? Suppliers { get; set; }
  }
}
