
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;


public class Supplier
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public int LeadTimeDays { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public bool IsActive { get; set; }

  public Supplier()
  {
    Id = Guid.NewGuid();
  }
}
