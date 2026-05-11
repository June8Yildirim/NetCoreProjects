
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;

public class Warehouse
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public string WarehouseCode { get; set; }

  [Required]
  public string Name { get; set; }

  public List<User> Employees { get; set; } = new();

  public Warehouse()
  {
    Id = Guid.NewGuid();
  }
}
