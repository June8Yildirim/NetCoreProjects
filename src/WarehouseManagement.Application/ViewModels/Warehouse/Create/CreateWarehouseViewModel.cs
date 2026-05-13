using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Application.ViewModels.Warehouse.Create
{

  public class CreateWarehouseViewModel
  {
    public Guid? Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Warehouse Code")]
    public string WarehouseCode { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Capacity Square Feet")]
    public int CapacitySquareFeet { get; set; }


    [Required]
    [Display(Name = "Current Utilization Percent")]
    public decimal? CurrentUtilizationPercent { get; set; }

    [Required]
    [Display(Name = "Timezone")]
    public string Timezone { get; set; } = string.Empty;




  }
}
