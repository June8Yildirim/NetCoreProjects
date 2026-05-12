
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class Supplier : BaseEntity
  {
    public string Name { get; set; } = string.Empty;
    public int LeadTimeDays { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
  }
}
// namespace WarehouseManagement.Core;
//
//
// public class Supplier
// {
//   [Key]
//   public Guid Id { get; set; }
//
//   [Required]
//   public int LeadTimeDays { get; set; }
//
//   [Required]
//   public string Name { get; set; }
//
//   [Required]
//   public bool IsActive { get; set; }
//
//   public Supplier()
//   {
//     Id = Guid.NewGuid();
//   }
// }
