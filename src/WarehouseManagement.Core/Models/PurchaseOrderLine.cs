using System;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class PurchaseOrderLine : BaseEntity
  {
    public Guid PurchaseOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int QuantityOrdered { get; set; }
    public int QuantityReceived { get; set; }
    public decimal UnitPrice { get; set; }

    // Navigation properties
    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
  }
}
