
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;

public class Permission
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public string Name { get; set; }

  public Permission()
  {
    Id = Guid.NewGuid();
  }
}
