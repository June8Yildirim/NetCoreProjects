using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Application.ViewModels.User.Login;
using WarehouseManagement.Application.ViewModels.User.Signup;
using WarehouseManagement.Core;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


public interface IUserService
{
  Task<SignupUserViewModel> SignupUserViewModel();
  Task SignupUserAsync(SignupUserViewModel model);

  Task<LoginUserViewModel> LoginUserViewModel();
  Task LoginUserAsync(LoginUserViewModel model);

  // Task<List<ListInventoryViewModel>> GetList3InventoryAsync();
  //
  // Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id);
  //
  // Task<CreateInventoryViewModel> CreateInventoryViewModelAsync();
  //
  // Task CreateInventoryAsync(CreateInventoryViewModel model);
  //
  // Task<int> GetTotalInventoriesCountAsync();
}
