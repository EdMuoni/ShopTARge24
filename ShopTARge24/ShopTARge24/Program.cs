using ShopTARge24.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.ApplicationServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the Spaceship service for dependency injection.
builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();

// This line was added to register the Kindergarten service for dependency injection.
// This resolves the "could not be found" and "no implicit conversion" errors.
builder.Services.AddScoped<IKindergartenServices, KindergartenServices>();

// This line registers the main database context for Spaceships.
builder.Services.AddDbContext<ShopTARge24Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ShopTARge24.Data")));

// This line was added to register the Kindergarten database context.
// This resolves the "Unable to resolve service" error for KindergartenContext.
builder.Services.AddDbContext<KindergartenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ShopTARge24.Data")));
var app = builder.Build();

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
