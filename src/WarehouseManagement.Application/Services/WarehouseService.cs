using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Application.Services;


public class WarehouseService : IWarehouseService
{
  private readonly WarehouseDbContext _context;

  public WarehouseService(WarehouseDbContext context)
  {
    _context = context;
  }
  public async Task<List<ListWarehouseViewModel>> GetAllWarehousesAsync()
  {
    return await _context.Warehouses
      .AsNoTracking()
      .Select(w => new ListWarehouseViewModel
      {
        Id = w.Id,
        Name = w.Name,
        WarehouseCode = w.WarehouseCode

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
        WarehouseCode = w.WarehouseCode
      })
    .FirstOrDefaultAsync();
  }
}
