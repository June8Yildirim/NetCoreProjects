using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

public class InventoryController : Controller
{
  private readonly IInventoryService _inventoryService;
  public InventoryController(IInventoryService service)
  {
    _inventoryService = service;
  }
  public async Task<IActionResult> Index()
  {
    var inventories = await _inventoryService.GetListInventoryAsync();
    if (inventories == null)
    {
      return NotFound();
    }

    return View(inventories);
  }

  public async Task<IActionResult> Details(Guid Id)
  {
    var inventory = await _inventoryService.GetInventoryByIdAsync(Id);
    if (inventory == null)
    {
      return NotFound();
    }
    return View(inventory);
  }

  [HttpGet]
  public async Task<IActionResult> GetDetailsPartial(Guid id)
  {
    var item = await _inventoryService.GetInventoryByIdAsync(id);
    if (item == null)
    {
      return NotFound();
    }
    return PartialView("_InventoryDetailsPartial", item);
  }


  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
