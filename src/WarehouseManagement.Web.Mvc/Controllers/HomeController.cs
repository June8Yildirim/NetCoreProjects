using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels.CompositeViewModels;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

public class HomeController : Controller
{
  private readonly IWarehouseService _warehouseService;
  private readonly IProductService _productService;
  private readonly IInventoryService _inventoryService;
  public HomeController(IWarehouseService warehouseService, IProductService productService, IInventoryService inventoryService)
  {
    _warehouseService = warehouseService;
    _productService = productService;
    _inventoryService = inventoryService;
  }
  public async Task<IActionResult> Index()
  {
    var warehouses = await _warehouseService.GetAllWarehousesAsync();
    var products = await _productService.GetAllProductsList();
    var inventories = await _inventoryService.GetListInventoryAsync();

    var homeModels = new HomeContentViewModel
    {
      Warehouses = warehouses,
      Products = products,
      Inventories = inventories
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
