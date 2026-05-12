using Microsoft.AspNetCore.Identity;
using WarehouseManagement.Application.ViewModels.User.Login;
using WarehouseManagement.Application.ViewModels.User.Signup;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly IWarehouseService _warehouseService;

  public UserService(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IWarehouseService warehouseService)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _warehouseService = warehouseService;
  }

  /// <summary>
  /// Prepares the registration view model by fetching all active warehouses.
  /// This ensures the user can select their assigned warehouse during signup.
  /// </summary>
  public async Task<SignupUserViewModel> SignupUserViewModel()
  {
    return new SignupUserViewModel
    {
      Warehouses = await _warehouseService.GetAllWarehousesAsync()
    };
  }

  /// <summary>
  /// Creates a new Identity User. 
  /// 1. Maps the ViewModel to the User entity.
  /// 2. Uses UserManager to hash the password and save to the database.
  /// 3. Throws an exception with detailed errors if registration fails.
  /// </summary>
  public async Task SignupUserAsync(SignupUserViewModel model)
  {
    var user = new User
    {
      UserName = model.Email,
      Email = model.Email,
      Name = model.Name,
      WarehouseId = model.WarehouseId,
      Position = "Employee" // Default position
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new Exception($"User creation failed: {errors}");
    }
  }

  /// <summary>
  /// Returns a fresh LoginUserViewModel.
  /// </summary>
  public Task<LoginUserViewModel> LoginUserViewModel()
  {
    return Task.FromResult(new LoginUserViewModel());
  }

  /// <summary>
  /// Authenticates the user via SignInManager.
  /// 1. Attempts to sign in with email and password.
  /// 2. If successful, ASP.NET Core issues an encrypted authentication cookie.
  /// 3. Lockout is currently disabled for simplicity.
  /// </summary>
  public async Task LoginUserAsync(LoginUserViewModel model)
  {
    var result = await _signInManager.PasswordSignInAsync(
        model.Email,
        model.Password,
        model.RememberMe,
        lockoutOnFailure: false);

    if (!result.Succeeded)
    {
      throw new Exception("Invalid login attempt.");
    }
  }

  /// <summary>
  /// Clears the authentication cookie from the user's browser via SignInManager.
  /// </summary>
  public async Task SignOutAsync()
  {
    await _signInManager.SignOutAsync();
  }
}
