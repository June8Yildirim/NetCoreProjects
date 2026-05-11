namespace WarehouseManagement.Core;

using System.ComponentModel.DataAnnotations;


public class User
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public string position { get; set; }

  [Required]
  public Guid WarehouseId { get; set; }

  public List<Permission> Permissions { get; set; } = new();
  public User()
  {
    Id = Guid.NewGuid();
  }
}
