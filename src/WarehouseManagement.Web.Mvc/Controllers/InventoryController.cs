using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Models;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

[Authorize]
public class InventoryController : Controller
{
  private readonly IInventoryService _inventoryService;
  private readonly UserManager<User> _userManager;
  public InventoryController(IInventoryService service, UserManager<User> userManager)
  {
    _inventoryService = service;
    _userManager = userManager;
  }
  public async Task<IActionResult> Index()
  {
    var user = await _userManager.GetUserAsync(User);
    Guid? warehouseId = user?.Position == "Employee" ? user.WarehouseId : null;

    var inventories = await _inventoryService.GetListInventoryAsync(warehouseId);
    if (inventories == null)
    {
      return NotFound();
    }

    return View(inventories);
  }

  public async Task<IActionResult> Details(Guid Id)
  {
    var user = await _userManager.GetUserAsync(User);
    Guid? warehouseId = user?.Position == "Employee" ? user.WarehouseId : null;

    var inventory = await _inventoryService.GetInventoryByIdAsync(Id, warehouseId);
    if (inventory == null)
    {
      return NotFound();
    }
    return View(inventory);
  }

  [HttpGet]
  public async Task<IActionResult> GetDetailsPartial(Guid id)
  {
    var user = await _userManager.GetUserAsync(User);
    Guid? warehouseId = user?.Position == "Employee" ? user.WarehouseId : null;

    var item = await _inventoryService.GetInventoryByIdAsync(id, warehouseId);
    if (item == null)
    {
      return NotFound();
    }
    return PartialView("_InventoryDetailsPartial", item);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null || (user.Position != "Regional Manager" && user.Position != "Warehouse Lead" && user.Position != "Owner"))
    {
      return Forbid();
    }
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
    var user = await _userManager.GetUserAsync(User);
    if (user == null || (user.Position != "Regional Manager" && user.Position != "Warehouse Lead" && user.Position != "Owner"))
    {
      return Forbid();
    }

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

  [HttpGet]
  public async Task<IActionResult> Edit(Guid id)
  {
    var viewModel = await _inventoryService.GetInventoryForEditAsync(id);
    if (viewModel == null) return NotFound();

    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateInventoryPartial", viewModel);
    }
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(CreateInventoryViewModel model)
  {
    if (ModelState.IsValid)
    {
      await _inventoryService.UpdateInventoryAsync(model);
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        return Json(new { success = true });
      }
      return RedirectToAction(nameof(Index));
    }

    var viewModel = await _inventoryService.CreateInventoryViewModelAsync();
    viewModel.Id = model.Id;
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
