using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.ViewModels.User.Login;
using WarehouseManagement.Application.ViewModels.User.Signup;

namespace WarehouseManagement.Web.Mvc.Controllers;

public class AccountController : Controller
{
  private readonly IUserService _userService;

  public AccountController(IUserService userService)
  {
    _userService = userService;
  }

  /// <summary>
  /// Displays the Signup page to unauthenticated users.
  /// </summary>
  [HttpGet]
  [AllowAnonymous]
  public async Task<IActionResult> Signup()
  {
    var model = await _userService.SignupUserViewModel();
    return View(model);
  }

  /// <summary>
  /// Processes the signup request.
  /// If successful, redirects to the Login page.
  /// If unsuccessful, re-populates the warehouse dropdown and shows errors.
  /// </summary>
  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Signup(SignupUserViewModel model)
  {
    if (ModelState.IsValid)
    {
      try
      {
        await _userService.SignupUserAsync(model);
        return RedirectToAction("Login");
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, ex.Message);
      }
    }

    model.Warehouses = (await _userService.SignupUserViewModel()).Warehouses;
    return View(model);
  }

  /// <summary>
  /// Displays the Login page. 
  /// Captures the 'returnUrl' to redirect the user back to their intended page after logging in.
  /// </summary>
  [HttpGet]
  [AllowAnonymous]
  public async Task<IActionResult> Login(string? returnUrl = null)
  {
    ViewData["ReturnUrl"] = returnUrl;
    var model = await _userService.LoginUserViewModel();
    return View(model);
  }

  /// <summary>
  /// Processes the login request.
  /// On success, redirects to either the 'returnUrl' (if local) or the Home Dashboard.
  /// </summary>
  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginUserViewModel model, string? returnUrl = null)
  {
    ViewData["ReturnUrl"] = returnUrl;
    if (ModelState.IsValid)
    {
      try
      {
        await _userService.LoginUserAsync(model);
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
          return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, ex.Message);
      }
    }

    return View(model);
  }

  /// <summary>
  /// Handles the logout request and redirects to the public Home page.
  /// Requires authentication.
  /// </summary>
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _userService.SignOutAsync();
    return RedirectToAction("Index", "Home");
  }
}
