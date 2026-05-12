using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels.Product.Create;
using WarehouseManagement.Web.Mvc.Models;

namespace WarehouseManagement.Web.Mvc.Controllers;

[Authorize]
public class ProductController : Controller
{
  private readonly IProductService _productService;

  public ProductController(IProductService service)
  {
    _productService = service;
  }
  public async Task<IActionResult> Index()
  {
    var productList = await _productService.GetAllProductsList();
    if (productList == null)
    {
      return NotFound();
    }
    return View(productList);
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

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var viewModel = await _productService.CreateProductViewModelAsync();
    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateProductPartial", viewModel);
    }
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CreateProductViewModel model)
  {
    if (ModelState.IsValid)
    {
      await _productService.CreateProductAsync(model);
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        return Json(new { success = true });
      }
      return RedirectToAction(nameof(Index));
    }

    var viewModel = await _productService.CreateProductViewModelAsync();
    viewModel.Name = model.Name;
    viewModel.SKU = model.SKU;
    viewModel.ReorderLevel = model.ReorderLevel;
    viewModel.UnitCost = model.UnitCost;
    viewModel.WeightLbs = model.WeightLbs;
    viewModel.Category = model.Category;
    viewModel.Barcode = model.Barcode;
    viewModel.SupplierId = model.SupplierId;

    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
      return PartialView("_CreateProductPartial", viewModel);
    }
    return View(viewModel);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
