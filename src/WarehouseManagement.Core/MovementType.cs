using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core
{

  public enum MovementType
  {
    [Display(Name = "Purchase/Inbound")]
    Inbound = 1,

    [Display(Name = "Sale/Outbound")]
    Outbound = 2,

    [Display(Name = "Inventory Adjustment")]
    Adjustment = 3,

    [Display(Name = "Damaged Goods")]
    Damaged = 4,

    [Display(Name = "Internal Transfer")]
    Transfer = 5
  }
}
