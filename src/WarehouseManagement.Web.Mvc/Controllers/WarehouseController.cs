using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels.Warehouse.Create;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

[Authorize]
public class WarehouseController : Controller
{
  private readonly IWarehouseService _warehouseService;

  public WarehouseController(IWarehouseService service)
  {
    _warehouseService = service;
  }

  public async Task<IActionResult> Index()
  {
    var warehouses = await _warehouseService.GetAllWarehousesAsync();
    if (warehouses == null)
    {
      return NotFound();
    }
    return View(warehouses);
  }

  public async Task<IActionResult> WarehouseDetails(Guid Id)
  {
    var warehouse = await _warehouseService.GetWarehouseByIdViewModelAsync(Id);
    if (warehouse == null)
    {
      return NotFound();
    }
    return PartialView("_WarehouseDetailsPartial", warehouse);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var viewModel = await _warehouseService.CreateWarehouseViewModelAsync();
    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateWarehousePartial", viewModel);
    }
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CreateWarehouseViewModel model)
  {
    if (ModelState.IsValid)
    {
      await _warehouseService.CreateWarehouseAsync(model);
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        return Json(new { success = true });
      }
      return RedirectToAction(nameof(Index));
    }

    var warehouseModel = await _warehouseService.CreateWarehouseViewModelAsync();
    warehouseModel.Name = model.Name;
    warehouseModel.CapacitySquareFeet = model.CapacitySquareFeet;
    warehouseModel.CurrentUtilizationPercent = model.CurrentUtilizationPercent;
    warehouseModel.Timezone = model.Timezone;
    warehouseModel.WarehouseCode = model.WarehouseCode;

    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateWarehousePartial", warehouseModel);
    }
    return View(warehouseModel);
  }
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
