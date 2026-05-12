using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Data;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


public class InventoryService : IInventoryService
{
  private readonly WarehouseDbContext _context;

  public InventoryService(WarehouseDbContext context)
  {
    _context = context;
  }
  public async Task<List<ListInventoryViewModel>> GetList3InventoryAsync()
  {
    return await _context.Inventories
      .Include(i => i.Product)
      .Include(i => i.Warehouse)
      .AsNoTracking()
      .Take(3)
      .Select(i => new ListInventoryViewModel
      {
        Id = i.Id,
        ProductName = i.Product.Name,
        WarehouseName = i.Warehouse.Name,
        ProductId = i.ProductId,
        WarehouseId = i.WarehouseId,
        QuantityOnHand = i.QuantityOnHand,
        QuantityAllocated = i.QuantityAllocated
      })
    .ToListAsync();
  }
  public async Task<List<ListInventoryViewModel>> GetListInventoryAsync()
  {
    return await _context.Inventories
      .Include(i => i.Product)
      .Include(i => i.Warehouse)
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

  public async Task<int> GetTotalInventoriesCountAsync()
  {
    return await _context.Inventories.CountAsync();
  }
}
