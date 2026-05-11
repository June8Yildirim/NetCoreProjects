using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

public class WarehouseController : Controller
{
  private readonly IWarehouseService _warehouseService;

  public WarehouseController(IWarehouseService service)
  {
    _warehouseService = service;
  }

  public IActionResult Index()
  {
    return View();
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

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
