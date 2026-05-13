using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Application.ViewModels.Product.Create;

namespace WarehouseManagement.Application.Services;

/// <summary>
/// This service handles everything related to Product management.
/// It acts as a middleman between the database and the UI to ensure
/// data is formatted correctly and business rules are followed.
/// </summary>
public interface IProductService
{
  /// <summary>
  /// Gets a preview list of 3 products, usually for a dashboard.
  /// </summary>
  /// <returns>A list of 3 products.</returns>
  Task<List<ListProductViewModel>> Get3ProductsList();

  /// <summary>
  /// Gets the full catalog of products.
  /// </summary>
  /// <returns>A list of all products in the system.</returns>
  Task<List<ListProductViewModel>> GetAllProductsList();

  /// <summary>
  /// Finds a specific product by its ID to show its full details.
  /// </summary>
  /// <param name="id">The unique ID of the product.</param>
  /// <returns>The product details or null if not found.</returns>
  Task<ProductDetailsViewModel?> GetProductByIdAsync(Guid id);

  /// <summary>
  /// Prepares the "Empty Form" for creating a new product.
  /// It pre-loads the list of Suppliers so the user can select one.
  /// </summary>
  /// <returns>A new ViewModel ready for the Product Create form.</returns>
  Task<CreateProductViewModel> CreateProductViewModelAsync();

  /// <summary>
  /// Saves a new product to the database.
  /// </summary>
  /// <param name="model">The validated form data from the user.</param>
  Task CreateProductAsync(CreateProductViewModel model);

  /// <summary>
  /// Prepares a ViewModel populated with existing product data for editing.
  /// </summary>
  /// <param name="id">The ID of the product to edit.</param>
  /// <returns>A populated CreateProductViewModel or null.</returns>
  Task<CreateProductViewModel?> GetProductForEditAsync(Guid id);

  /// <summary>
  /// Updates an existing product in the database.
  /// </summary>
  /// <param name="model">The updated product data.</param>
  Task UpdateProductAsync(CreateProductViewModel model);

  /// <summary>
  /// Counts how many products are currently in our catalog.
  /// </summary>
  /// <returns>Total number of products.</returns>
  Task<int> GetTotalProductsCountAsync();
}
