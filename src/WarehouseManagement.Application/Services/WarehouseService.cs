using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Application.Services;


public class WarehouseService : IWarehouseService
{
  private readonly WarehouseDbContext _context;

  public WarehouseService(WarehouseDbContext context)
  {
    _context = context;
  }
  public async Task<List<ListWarehouseViewModel>> Get3WarehousesAsync()
  {
    return await _context.Warehouses
      .AsNoTracking()
      .Take(3)
      .Select(w => new ListWarehouseViewModel
      {
        Id = w.Id,
        Name = w.Name,
        WarehouseCode = w.WarehouseCode

      }).ToListAsync();
  }
  public async Task<List<ListWarehouseViewModel>> GetAllWarehousesAsync()
  {
    return await _context.Warehouses
      .AsNoTracking()
      .Select(w => new ListWarehouseViewModel
      {
        Id = w.Id,
        Name = w.Name,
        WarehouseCode = w.WarehouseCode,
        CapacitySquareFeet = w.CapacitySquareFeet ?? 0,
        TotalInventories = w.Inventories.Count(),
        TotalUsers = w.Users.Count(),
        FromTransfers = w.FromTransfers,
        ToTransfers = w.ToTransfers

      }).ToListAsync();
  }

  public async Task<WarehouseByIdViewModel> GetWarehouseByIdViewModelAsync(Guid Id)
  {
    return await _context.Warehouses
      .AsNoTracking()
      .Where(w => w.Id == Id)
      .Select(w => new WarehouseByIdViewModel
      {
        Id = w.Id,
        Name = w.Name,
        WarehouseCode = w.WarehouseCode,
        CapacitySquareFeet = w.CapacitySquareFeet ?? 0,
        Timezone = w.Timezone,
        Inventories = w.Inventories,
        Users = w.Users,
        FromTransfers = w.FromTransfers,
        ToTransfers = w.ToTransfers
      })
    .FirstOrDefaultAsync();
  }

  public async Task<int> GetTotalWarehousesCountAsync()
  {
    return await _context.Warehouses.CountAsync();
  }
}
