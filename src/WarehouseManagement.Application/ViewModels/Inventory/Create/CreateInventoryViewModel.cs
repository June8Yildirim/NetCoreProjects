using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// This ViewModel is a "Data Carrier" specifically designed for the 
/// Create Inventory form. It only contains the fields the user needs 
/// to fill out, plus the lists needed for the dropdown menus.
/// </summary>
namespace WarehouseManagement.Application.ViewModels
{
  public class CreateInventoryViewModel
  {
    /// <summary>
    /// The ID of the product the user selected from the dropdown.
    /// [Required] ensures the form cannot be submitted without a selection.
    /// </summary>
    [Required]
    [Display(Name = "Product")]
    public Guid ProductId { get; set; }

    /// <summary>
    /// The ID of the warehouse where this stock is located.
    /// </summary>
    [Required]
    [Display(Name = "Warehouse")]
    public Guid WarehouseId { get; set; }

    /// <summary>
    /// How many units are currently physically present.
    /// [Range] ensures we don't accidentally enter negative numbers.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Quantity On Hand")]
    public int QuantityOnHand { get; set; }

    /// <summary>
    /// Optional: The threshold at which we should be alerted to restock.
    /// </summary>
    [Range(0, int.MaxValue)]
    [Display(Name = "Min Safety Stock")]
    public int? MinSafetyStock { get; set; }

    /// <summary>
    /// This list holds the "options" for the Product dropdown menu.
    /// It is populated by the Service before showing the form.
    /// </summary>
    public IEnumerable<SelectListItem>? Products { get; set; }

    /// <summary>
    /// This list holds the "options" for the Warehouse dropdown menu.
    /// </summary>
    public IEnumerable<SelectListItem>? Warehouses { get; set; }
  }
}
