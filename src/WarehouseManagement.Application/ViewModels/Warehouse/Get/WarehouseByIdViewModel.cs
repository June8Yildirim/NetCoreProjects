
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.ViewModels
{

  public class WarehouseByIdViewModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? WarehouseCode { get; set; }
    public int? CapacitySquareFeet { get; set; }
    public decimal? CurrentUtilizationPercent { get; set; }
    public string? Timezone { get; set; }

    // Navigation properties
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public virtual ICollection<WarehouseManagement.Models.User> Users { get; set; } = new List<WarehouseManagement.Models.User>();
    public virtual ICollection<Transfer> FromTransfers { get; set; } = new List<Transfer>();
    public virtual ICollection<Transfer> ToTransfers { get; set; } = new List<Transfer>();

  }
}
