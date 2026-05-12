using System;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public enum TransferStatus
  {
    Pending = 1,
    InTransit = 2,
    Received = 3,
    Cancelled = 4
  }

  public class Transfer : BaseEntity
  {
    public string TransferNumber { get; set; } = string.Empty;
    public Guid FromWarehouseId { get; set; }
    public Guid ToWarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public TransferStatus Status { get; set; } = TransferStatus.Pending;
    public DateTime? ShippedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }

    // Navigation properties
    public virtual Warehouse FromWarehouse { get; set; } = null!;
    public virtual Warehouse ToWarehouse { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
  }
}
