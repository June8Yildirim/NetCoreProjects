using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Core;

namespace WarehouseManagement.Application.Services;


public interface IInventoryService
{
  Task<List<ListInventoryViewModel>> GetListInventoryAsync();
  Task<List<ListInventoryViewModel>> GetList3InventoryAsync();
  Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id);
  Task<int> GetTotalInventoriesCountAsync();
}
