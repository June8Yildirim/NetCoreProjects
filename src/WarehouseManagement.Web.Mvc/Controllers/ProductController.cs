using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

public class ProductController : Controller
{
  private readonly IProductService _productService;

  public ProductController(IProductService service)
  {
    _productService = service;
  }
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public async Task<IActionResult> ProductDetails(Guid Id)
  {
    var product = await _productService.GetProductByIdAsync(Id);
    if (product == null)
    {
      return NotFound();
    }
    return PartialView("_ProductDetailsPartial", product);
  }
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
