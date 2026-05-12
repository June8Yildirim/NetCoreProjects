using System;
using System.Collections.Generic;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class Warehouse : BaseEntity
  {
    public string WarehouseCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? CapacitySquareFeet { get; set; }
    public decimal? CurrentUtilizationPercent { get; set; }
    public string? Timezone { get; set; }

    // Navigation properties
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Transfer> FromTransfers { get; set; } = new List<Transfer>();
    public virtual ICollection<Transfer> ToTransfers { get; set; } = new List<Transfer>();
  }
}
