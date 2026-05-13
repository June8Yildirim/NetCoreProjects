using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Data;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.ViewModels.Warehouse.Create;
using WarehouseManagement.Models;

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

  public async Task<WarehouseDetailsViewModel?> GetWarehouseDetailsViewModelAsync(Guid Id)
  {
    return await _context.Warehouses
      .AsNoTracking()
      .Where(w => w.Id == Id)
      .Select(w => new WarehouseDetailsViewModel
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

  /// <summary>
  /// Database total counter.
  /// </summary>
  public async Task<int> GetTotalWarehousesCountAsync()
  {
    return await _context.Warehouses.CountAsync();
  }

  /// <summary>
  /// Prepares an empty form for creating a new warehouse.
  /// </summary>
  public Task<CreateWarehouseViewModel> CreateWarehouseViewModelAsync()
  {
    return Task.FromResult(new CreateWarehouseViewModel());
  }

  /// <summary>
  /// Saves the new warehouse to the database.
  /// </summary>
  public async Task CreateWarehouseAsync(CreateWarehouseViewModel model)
  {
    var warehouse = new Warehouse
    {
      Name = model.Name,
      CapacitySquareFeet = model.CapacitySquareFeet,
      Timezone = model.Timezone,
      WarehouseCode = model.WarehouseCode,
      CurrentUtilizationPercent = model.CurrentUtilizationPercent
    };
    _context.Warehouses.Add(warehouse);
    await _context.SaveChangesAsync();
  }

  public async Task<CreateWarehouseViewModel?> GetWarehouseForEditAsync(Guid id)
  {
    var warehouse = await _context.Warehouses
        .AsNoTracking()
        .FirstOrDefaultAsync(w => w.Id == id);

    if (warehouse == null) return null;

    return new CreateWarehouseViewModel
    {
      Id = warehouse.Id,
      Name = warehouse.Name,
      WarehouseCode = warehouse.WarehouseCode ?? "",
      CapacitySquareFeet = warehouse.CapacitySquareFeet ?? 0,
      CurrentUtilizationPercent = warehouse.CurrentUtilizationPercent,
      Timezone = warehouse.Timezone ?? ""
    };
  }

  public async Task UpdateWarehouseAsync(CreateWarehouseViewModel model)
  {
    var warehouse = await _context.Warehouses.FindAsync(model.Id);
    if (warehouse == null) throw new Exception("Warehouse not found");

    warehouse.Name = model.Name;
    warehouse.WarehouseCode = model.WarehouseCode;
    warehouse.CapacitySquareFeet = model.CapacitySquareFeet;
    warehouse.CurrentUtilizationPercent = model.CurrentUtilizationPercent;
    warehouse.Timezone = model.Timezone;

    _context.Warehouses.Update(warehouse);
    await _context.SaveChangesAsync();
  }
}
