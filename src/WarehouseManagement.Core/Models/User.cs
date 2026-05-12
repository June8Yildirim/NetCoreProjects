using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WarehouseManagement.Models
{
  public class User : IdentityUser<Guid>
  {
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Position { get; set; } = "Employee";

    public Guid WarehouseId { get; set; }
    
    // Navigation properties
    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual ICollection<StockTracking> StockTrackings { get; set; } = new List<StockTracking>();
    
    // Manual audit fields (since we can't inherit from BaseEntity and IdentityUser at the same time)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
  }
}
