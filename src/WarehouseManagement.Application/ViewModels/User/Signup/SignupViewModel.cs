using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Application.ViewModels.User.Signup
{

  public class SignupUserViewModel
  {
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "WarehouseId")]
    public Guid WarehouseId { get; set; }
  }
}
