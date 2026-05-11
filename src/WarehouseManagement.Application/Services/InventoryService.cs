using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.EntityFrameworkCore;

namespace WarehouseManagement.Application.Services;


public class InventoryService : IInventoryService
{
  private readonly WarehouseDbContext _context;

  public InventoryService(WarehouseDbContext context)
  {
    _context = context;
  }
  public async Task<List<ListInventoryViewModel>> GetListInventoryAsync()
  {
    return await _context.Inventories
      .AsNoTracking()
      .Select(i => new ListInventoryViewModel
      {
        Id = i.Id,
        ProductName = i.Product.Name,
        WarehouseName = i.Warehouse.Name,
        ProductId = i.ProductId,
        WarehouseId = i.WarehouseId,
        QuantityOnHand = i.QuantityOnHand,
        QuantityAllocated = i.QuantityAllocated
      }).ToListAsync();
  }

  public async Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id)
  {
    return await _context.Inventories
      .AsNoTracking()
      .Where(i => i.Id == id)
      .Select(i => new InventoryViewModel
      {
        Id = i.Id,
        ProductName = i.Product.Name,
        WarehouseName = i.Warehouse.Name,
        ProductId = i.ProductId,
        WarehouseId = i.WarehouseId,
        QuantityOnHand = i.QuantityOnHand,
        QuantityAllocated = i.QuantityAllocated
      }).FirstOrDefaultAsync();
  }
}
