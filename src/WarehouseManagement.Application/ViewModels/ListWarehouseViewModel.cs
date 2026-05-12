using WarehouseManagement.Models;

namespace WarehouseManagement.Application.ViewModels;

public class ListWarehouseViewModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? WarehouseCode { get; set; }
  public int CapacitySquareFeet { get; set; }
  public int TotalInventories { get; set; }
  public int TotalUsers { get; set; }
  public ICollection<Transfer> FromTransfers { get; set; }
  public ICollection<Transfer> ToTransfers { get; set; }

}
