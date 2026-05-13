using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels.CompositeViewModels;
using WarehouseManagement.Models;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

[Authorize]
public class HomeController : Controller
{
  private readonly IWarehouseService _warehouseService;
  private readonly IProductService _productService;
  private readonly IInventoryService _inventoryService;
  private readonly UserManager<User> _userManager;

  public HomeController(
      IWarehouseService warehouseService, 
      IProductService productService, 
      IInventoryService inventoryService,
      UserManager<User> userManager)
  {
    _warehouseService = warehouseService;
    _productService = productService;
    _inventoryService = inventoryService;
    _userManager = userManager;
  }

  public async Task<IActionResult> Index()
  {
    var user = await _userManager.GetUserAsync(User);
    Guid? warehouseId = user?.Position == "Employee" ? user.WarehouseId : null;

    var threeWarehouses = await _warehouseService.Get3WarehousesAsync();
    var threeProducts = await _productService.Get3ProductsList();
    var threeInventories = await _inventoryService.GetList3InventoryAsync(warehouseId);

    var totalWarehouses = await _warehouseService.GetTotalWarehousesCountAsync();
    var totalProducts = await _productService.GetTotalProductsCountAsync();
    var totalInventories = await _inventoryService.GetTotalInventoriesCountAsync();

    var homeModels = new HomeContentViewModel
    {
      ThreeWarehouses = threeWarehouses,
      ThreeProducts = threeProducts,
      ThreeInventories = threeInventories,
      TotalWarehouses = totalWarehouses,
      TotalProducts = totalProducts,
      TotalInventories = totalInventories
    };
    return View(homeModels);
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
