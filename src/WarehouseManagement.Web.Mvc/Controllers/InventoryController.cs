using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

[Authorize]
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

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var viewModel = await _inventoryService.CreateInventoryViewModelAsync();
    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateInventoryPartial", viewModel);
    }
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CreateInventoryViewModel model)
  {
    if (ModelState.IsValid)
    {
      await _inventoryService.CreateInventoryAsync(model);
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        return Json(new { success = true });
      }
      return RedirectToAction(nameof(Index));
    }

    var viewModel = await _inventoryService.CreateInventoryViewModelAsync();
    // Re-populate data if validation fails
    viewModel.ProductId = model.ProductId;
    viewModel.WarehouseId = model.WarehouseId;
    viewModel.QuantityOnHand = model.QuantityOnHand;
    viewModel.MinSafetyStock = model.MinSafetyStock;

    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateInventoryPartial", viewModel);
    }
    return View(viewModel);
  }


  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
