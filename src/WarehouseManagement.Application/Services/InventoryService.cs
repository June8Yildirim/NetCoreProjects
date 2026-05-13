using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Data;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


/// <summary>
/// This class is the actual implementation of the IInventoryService.
/// It uses Entity Framework (WarehouseDbContext) to talk to the physical database.
/// </summary>
public class InventoryService : IInventoryService
{
  private readonly WarehouseDbContext _context;

  /// <summary>
  /// The constructor "injects" the database context so we can use it.
  /// </summary>
  public InventoryService(WarehouseDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Fetches 3 records. We use .AsNoTracking() here because we are only 
  /// reading data for display, which makes the query faster and saves memory.
  /// </summary>
  public async Task<List<ListInventoryViewModel>> GetList3InventoryAsync(Guid? warehouseId = null)
  {
    var query = _context.Inventories.AsQueryable();

    if (warehouseId.HasValue)
    {
      query = query.Where(i => i.WarehouseId == warehouseId.Value);
    }
    return await query
    .Include(i => i.Product)   // Tell EF to load the Product info too
    .Include(i => i.Warehouse) // Tell EF to load the Warehouse info too
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
      QuantityAllocated = i.QuantityAllocated,
      AvailableQuantity = i.AvailableQuantity,
      NeedsReorder = i.NeedsReorder
    })
  .ToListAsync();
  }

  /// <summary>
  /// Fetches all records. We "Select" them into a ViewModel to ensure the 
  /// View only receives the exact data it needs to show the table.
  /// </summary>
  public async Task<List<ListInventoryViewModel>> GetListInventoryAsync(Guid? warehouseId = null)
  {
    var query = _context.Inventories.AsQueryable();

    if (warehouseId.HasValue)
    {
      query = query.Where(i => i.WarehouseId == warehouseId.Value);
    }
    return await query
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
        QuantityAllocated = i.QuantityAllocated,
        AvailableQuantity = i.AvailableQuantity,
        NeedsReorder = i.NeedsReorder
      }).ToListAsync();
  }

  /// <summary>
  /// Finds one record. This is a "surgical" query to get a single row.
  /// </summary>
  public async Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id, Guid? warehouseId = null)
  {
    var query = _context.Inventories.AsQueryable();

    if (warehouseId.HasValue)
    {
      query = query.Where(i => i.WarehouseId == warehouseId.Value);
    }
    return await query
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
        QuantityAllocated = i.QuantityAllocated,
        MinSafetyStock = i.MinSafetyStock,
        AvailableQuantity = i.AvailableQuantity,
        NeedsReorder = i.NeedsReorder
      }).FirstOrDefaultAsync();
  }

  /// <summary>
  /// Database total counter.
  /// </summary>
  public async Task<int> GetTotalInventoriesCountAsync()
  {
    return await _context.Inventories.CountAsync();
  }

  /// <summary>
  /// This method is like a "Shopping List Generator". 
  /// Before the user can add inventory, they need to know what Products 
  /// and Warehouses exist. We fetch them here and format them into SelectListItems.
  /// </summary>
  public async Task<CreateInventoryViewModel> CreateInventoryViewModelAsync()
  {
    // Fetch all products and turn them into text/value pairs for a dropdown
    var products = await _context.Products
        .AsNoTracking()
        .Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = $"{p.SKU} - {p.Name}"
        })
        .ToListAsync();

    // Fetch all warehouses and turn them into text/value pairs for a dropdown
    var warehouses = await _context.Warehouses
        .AsNoTracking()
        .Select(w => new SelectListItem
        {
          Value = w.Id.ToString(),
          Text = w.Name
        })
        .ToListAsync();

    // Return the "Empty Form" with the dropdown lists filled
    return new CreateInventoryViewModel
    {
      Products = products,
      Warehouses = warehouses
    };
  }

  /// <summary>
  /// This is the final step of the "Create" process.
  /// We take the raw form data (the ViewModel) and convert it into a 
  /// real database entity (the Model) to be saved permanently.
  /// </summary>
  public async Task CreateInventoryAsync(CreateInventoryViewModel model)
  {
    var inventory = new Inventory
    {
      ProductId = model.ProductId,
      WarehouseId = model.WarehouseId,
      QuantityOnHand = model.QuantityOnHand,
      MinSafetyStock = model.MinSafetyStock,
      QuantityAllocated = 0, // New items start with 0 allocated
      LastCounted = DateTime.UtcNow // Set initial count date
    };

    _context.Inventories.Add(inventory);
    await _context.SaveChangesAsync();
  }

  public async Task<CreateInventoryViewModel?> GetInventoryForEditAsync(Guid id)
  {
    var inventory = await _context.Inventories
        .AsNoTracking()
        .FirstOrDefaultAsync(i => i.Id == id);

    if (inventory == null) return null;

    var viewModel = await CreateInventoryViewModelAsync();
    viewModel.Id = inventory.Id;
    viewModel.ProductId = inventory.ProductId;
    viewModel.WarehouseId = inventory.WarehouseId;
    viewModel.QuantityOnHand = inventory.QuantityOnHand;
    viewModel.MinSafetyStock = inventory.MinSafetyStock;

    return viewModel;
  }

  public async Task UpdateInventoryAsync(CreateInventoryViewModel model)
  {
    var inventory = await _context.Inventories.FindAsync(model.Id);
    if (inventory == null) throw new Exception("Inventory record not found");

    inventory.ProductId = model.ProductId;
    inventory.WarehouseId = model.WarehouseId;
    inventory.QuantityOnHand = model.QuantityOnHand;
    inventory.MinSafetyStock = model.MinSafetyStock;

    _context.Inventories.Update(inventory);
    await _context.SaveChangesAsync();
  }
}
