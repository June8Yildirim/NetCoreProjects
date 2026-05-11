using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Core;

namespace WarehouseManagement.Application.Services;


public interface IInventoryService
{
  Task<List<ListInventoryViewModel>> GetListInventoryAsync();
  Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id);
}
