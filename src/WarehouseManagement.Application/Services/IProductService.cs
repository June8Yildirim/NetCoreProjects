using WarehouseManagement.Application.ViewModels;

namespace WarehouseManagement.Application.Services;

public interface IProductService
{
  Task<List<ListProductViewModel>> Get3ProductsList();
  Task<List<ListProductViewModel>> GetAllProductsList();
  Task<ProductByIdViewModel?> GetProductByIdAsync(Guid id);
  Task<int> GetTotalProductsCountAsync();
}
