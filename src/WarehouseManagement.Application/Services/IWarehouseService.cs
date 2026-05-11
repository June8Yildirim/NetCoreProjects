using WarehouseManagement.Application.ViewModels;

namespace WarehouseManagement.Application.Services;

public interface IWarehouseService
{
  Task<List<ListWarehouseViewModel>> GetAllWarehousesAsync();
  Task<WarehouseByIdViewModel> GetWarehouseByIdViewModelAsync(Guid Id);
}
