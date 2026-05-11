
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.EntityFrameworkCore;

namespace WarehouseManagement.Application.Services;


public class ProductService : IProductService
{
  private readonly WarehouseDbContext _context;

  public ProductService(WarehouseDbContext context)
  {
    _context = context;
  }

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
        ReorderLevel = p.ReorderLevel
      }).ToListAsync();
  }

  public async Task<ProductByIdViewModel?> GetProductByIdAsync(Guid Id)
  {
    return await _context.Products
      .AsNoTracking()
      .Where(p => p.Id == Id)
      .Select(p => new ProductByIdViewModel
      {
        Id = p.Id,
        Name = p.Name,
        SKU = p.SKU,
        SupplierId = p.SupplierId,
        ReorderLevel = p.ReorderLevel
      }).FirstOrDefaultAsync();
  }
}
