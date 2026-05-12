using System;
using System.Collections.Generic;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class User : BaseEntity
  {
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public Guid WarehouseId { get; set; }
    public string? Email { get; set; }

    // Navigation properties
    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual ICollection<StockTracking> StockTrackings { get; set; } = new List<StockTracking>();
  }
}
// namespace WarehouseManagement.Core;
//
// using System.ComponentModel.DataAnnotations;
//
//
// public class User
// {
//   [Key]
//   public Guid Id { get; set; }
//
//   [Required]
//   public string Name { get; set; }
//
//   [Required]
//   public string position { get; set; }
//
//   [Required]
//   public Guid WarehouseId { get; set; }
//
//   public List<Permission> Permissions { get; set; } = new();
//   public User()
//   {
//     Id = Guid.NewGuid();
//   }
// }
