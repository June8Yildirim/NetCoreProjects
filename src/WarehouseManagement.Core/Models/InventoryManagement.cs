using System;
using System.Collections.Generic;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public enum PurchaseOrderStatus
  {
    Draft = 1,
    Submitted = 2,
    Approved = 3,
    Shipped = 4,
    Received = 5,
    Cancelled = 6
  }

  public class PurchaseOrder : BaseEntity
  {
    public string PONumber { get; set; } = string.Empty;
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;
    public decimal? TotalAmount { get; set; }

    // Navigation properties
    public virtual Supplier Supplier { get; set; } = null!;
    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; } = new List<PurchaseOrderLine>();
  }
}
