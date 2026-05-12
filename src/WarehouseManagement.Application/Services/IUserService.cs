using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Application.ViewModels.User.Login;
using WarehouseManagement.Application.ViewModels.User.Signup;
using WarehouseManagement.Core;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


public interface IUserService
{
  /// <summary>
  /// Prepares a ViewModel for the user registration (signup) form, 
  /// including the list of available warehouses for selection.
  /// </summary>
  /// <returns>A populated SignupUserViewModel.</returns>
  Task<SignupUserViewModel> SignupUserViewModel();

  /// <summary>
  /// Registers a new user in the system using ASP.NET Core Identity.
  /// </summary>
  /// <param name="model">The registration data submitted by the user.</param>
  /// <exception cref="Exception">Thrown if user creation fails.</exception>
  Task SignupUserAsync(SignupUserViewModel model);

  /// <summary>
  /// Prepares an empty ViewModel for the login form.
  /// </summary>
  /// <returns>A new LoginUserViewModel.</returns>
  Task<LoginUserViewModel> LoginUserViewModel();

  /// <summary>
  /// Authenticates a user and starts a secure session using SignInManager.
  /// </summary>
  /// <param name="model">The login credentials submitted by the user.</param>
  /// <exception cref="Exception">Thrown if authentication fails.</exception>
  Task LoginUserAsync(LoginUserViewModel model);

  /// <summary>
  /// Signs out the current user and clears the authentication cookie.
  /// </summary>
  Task SignOutAsync();
}
