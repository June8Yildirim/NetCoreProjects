
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Application.ViewModels.Product.Create;
using WarehouseManagement.Data;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


/// <summary>
/// This class is the actual logic for the Product Service.
/// It uses Entity Framework (WarehouseDbContext) to interact with the database.
/// </summary>
public class ProductService : IProductService
{
  private readonly WarehouseDbContext _context;

  public ProductService(WarehouseDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Optimized query to get just 3 products for the Dashboard view.
  /// </summary>
  public async Task<List<ListProductViewModel>> Get3ProductsList()
  {
    return await _context.Products
      .AsNoTracking()
      .Take(3)
      .Select(p => new ListProductViewModel
      {
        Id = p.Id,
        Name = p.Name,
        SKU = p.SKU,
        SupplierId = p.SupplierId,
        ReorderLevel = p.ReorderLevel
      }).ToListAsync();
  }

  /// <summary>
  /// Fetches all products and includes extra info like Supplier Name.
  /// This is used for the main Product Management table.
  /// </summary>
  public async Task<List<ListProductViewModel>> GetAllProductsList()
  {
    return await _context.Products
      .AsNoTracking()
      .Select(p => new ListProductViewModel
      {
        Id = p.Id,
        Name = p.Name,
        SKU = p.SKU,
        SupplierId = p.SupplierId,
        ReorderLevel = p.ReorderLevel,
        SupplierName = p.Supplier.Name,
        UnitCost = p.UnitCost ?? 0,
        WeightLbs = p.WeightLbs ?? 0,
        Category = p.Category ?? "",
        Barcode = p.Barcode ?? ""
      }).ToListAsync();
  }

  /// <summary>
  /// Fetches details for a single product.
  /// </summary>
  public async Task<ProductDetailsViewModel?> GetProductByIdAsync(Guid Id)
  {
    return await _context.Products
      .AsNoTracking()
      .Where(p => p.Id == Id)
      .Select(p => new ProductDetailsViewModel
      {
        Id = p.Id,
        Name = p.Name,
        SKU = p.SKU,
        SupplierId = p.SupplierId,
        ReorderLevel = p.ReorderLevel,
        UnitCost = p.UnitCost ?? 0,
        WeightLbs = p.WeightLbs ?? 0,
        Category = p.Category ?? "Not Registered",
        Barcode = p.Barcode ?? "Unknown",
        SupplierName = p.Supplier.Name,
        Inventories = p.Inventories,
        StockTrackings = p.StockTrackings,
        PurchaseOrderLines = p.PurchaseOrderLines
      })
      .FirstOrDefaultAsync();
  }

  /// <summary>
  /// Database total counter.
  /// </summary>
  public async Task<int> GetTotalProductsCountAsync()
  {
    return await _context.Products.CountAsync();
  }

  /// <summary>
  /// Prepares the form for a new product by fetching all current Suppliers
  /// so they can be shown in the "Supplier" dropdown list.
  /// </summary>
  public async Task<CreateProductViewModel> CreateProductViewModelAsync()
  {
    var suppliers = await _context.Suppliers
      .AsNoTracking()
      .Select(s => new SelectListItem
      {
        Value = s.Id.ToString(),
        Text = $"{s.Id} - {s.Name}"
      }).ToListAsync();

    return new CreateProductViewModel
    {
      Suppliers = suppliers
    };
  }

  /// <summary>
  /// The final save step. It maps the form data into a real database row.
  /// </summary>
  public async Task CreateProductAsync(CreateProductViewModel model)
  {
    var product = new WarehouseManagement.Models.Product
    {
      Name = model.Name,
      SKU = model.SKU,
      ReorderLevel = model.ReorderLevel,
      UnitCost = model.UnitCost,
      WeightLbs = model.WeightLbs,
      Category = model.Category,
      Barcode = model.Barcode,
      SupplierId = model.SupplierId
    };

    _context.Products.Add(product);
    await _context.SaveChangesAsync();
  }

  public async Task<CreateProductViewModel?> GetProductForEditAsync(Guid id)
  {
    var product = await _context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null) return null;

    var suppliers = await _context.Suppliers
        .AsNoTracking()
        .Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.Name
        }).ToListAsync();

    return new CreateProductViewModel
    {
      Id = product.Id,
      Name = product.Name,
      SKU = product.SKU,
      ReorderLevel = product.ReorderLevel,
      UnitCost = product.UnitCost,
      WeightLbs = product.WeightLbs,
      Category = product.Category,
      Barcode = product.Barcode,
      SupplierId = product.SupplierId,
      Suppliers = suppliers
    };
  }

  public async Task UpdateProductAsync(CreateProductViewModel model)
  {
    var product = await _context.Products.FindAsync(model.Id);
    if (product == null) throw new Exception("Product not found");

    product.Name = model.Name;
    product.SKU = model.SKU;
    product.ReorderLevel = model.ReorderLevel;
    product.UnitCost = model.UnitCost;
    product.WeightLbs = model.WeightLbs;
    product.Category = model.Category;
    product.Barcode = model.Barcode;
    product.SupplierId = model.SupplierId;

    _context.Products.Update(product);
    await _context.SaveChangesAsync();
  }
}
