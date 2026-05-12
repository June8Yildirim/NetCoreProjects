using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Application.ViewModels.User.Signup
{

  public class SignupUserViewModel
  {
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Warehouse")]
    public Guid WarehouseId { get; set; }

    public List<ListWarehouseViewModel>? Warehouses { get; set; }
  }
}
