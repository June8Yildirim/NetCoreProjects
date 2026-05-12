using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Application.ViewModels.Warehouse.Create;

namespace WarehouseManagement.Application.Services;

public interface IWarehouseService
{
  Task<List<ListWarehouseViewModel>> GetAllWarehousesAsync();
  Task<List<ListWarehouseViewModel>> Get3WarehousesAsync();
  /// <summary>
  /// Finds a specific warehouse by its ID for a detailed view.
  /// </summary>
  Task<WarehouseByIdViewModel?> GetWarehouseByIdViewModelAsync(Guid Id);

  /// <summary>
  /// Prepares an empty ViewModel for the Create Warehouse form.
  /// </summary>
  Task<CreateWarehouseViewModel> CreateWarehouseViewModelAsync();

  /// <summary>
  /// Saves a new warehouse to the database.
  /// </summary>
  Task CreateWarehouseAsync(CreateWarehouseViewModel model);

  /// <summary>
  /// Counts total number of warehouses.
  /// </summary>
  Task<int> GetTotalWarehousesCountAsync();
}
