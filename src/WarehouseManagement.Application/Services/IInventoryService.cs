using WarehouseManagement.Application.ViewModels;
using WarehouseManagement.Core;
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.Services;


/// <summary>
/// This service acts as the "brain" for all inventory-related operations.
/// It defines the contract for how our application interacts with inventory data,
/// ensuring that the Web/UI layer doesn't talk directly to the database.
/// </summary>
public interface IInventoryService
{
  /// <summary>
  /// Retrieves a full list of all inventory records in the system.
  /// It maps the database entities into a simplified 'ListInventoryViewModel' 
  /// that is easier for the user interface to display.
  /// </summary>
  /// <returns>A list of inventory items formatted for a table or list view.</returns>
  Task<List<ListInventoryViewModel>> GetListInventoryAsync();

  /// <summary>
  /// Retrieves exactly 3 inventory records, typically used for the Dashboard.
  /// This is optimized to only fetch a small subset of data.
  /// </summary>
  /// <returns>A small list of 3 inventory items.</returns>
  Task<List<ListInventoryViewModel>> GetList3InventoryAsync();

  /// <summary>
  /// Finds a specific inventory record by its unique ID.
  /// This is used when a user wants to see the full details of a single item.
  /// </summary>
  /// <param name="id">The unique GUID of the inventory record.</param>
  /// <returns>A detailed view model if found, otherwise null.</returns>
  Task<InventoryViewModel?> GetInventoryByIdAsync(Guid id);

  /// <summary>
  /// Prepares a fresh 'CreateInventoryViewModel' to be used in a form.
  /// This is a critical step: it goes to the database to fetch the list of 
  /// available Products and Warehouses so the user can choose from them in a dropdown.
  /// </summary>
  /// <returns>A ViewModel ready to be shown in the Create modal/page.</returns>
  Task<CreateInventoryViewModel> CreateInventoryViewModelAsync();

  /// <summary>
  /// Takes the data submitted by a user through a form and saves it as a 
  /// new record in the database. It handles the "mapping" from the form data
  /// back into the database model.
  /// </summary>
  /// <param name="model">The data captured from the user's form.</param>
  Task CreateInventoryAsync(CreateInventoryViewModel model);

  /// <summary>
  /// Simply counts how many inventory records we have in total.
  /// Useful for showing statistics or "Total Items" counters on the UI.
  /// </summary>
  /// <returns>The total count of inventory rows.</returns>
  Task<int> GetTotalInventoriesCountAsync();
}
