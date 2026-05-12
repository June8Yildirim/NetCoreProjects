using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Core;
using WarehouseManagement.EntityFrameworkCore;
using WarehouseManagement.Data;
using WarehouseManagement.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
      options.ViewLocationFormats.Add("/Views/Shared/Dashboard/{0}.cshtml");
      options.ViewLocationFormats.Add("/Views/Shared/Inventory/{0}.cshtml");
      options.ViewLocationFormats.Add("/Views/Shared/Product/{0}.cshtml");
      options.ViewLocationFormats.Add("/Views/Shared/Warehouse/{0}.cshtml");
    });

builder.Services.AddDbContext<WarehouseDbContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  options.UseMySQL(connectionString);
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
{
  opt.Password.RequireDigit = true;
  opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<WarehouseDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<WarehouseDbContext>();
  await DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
