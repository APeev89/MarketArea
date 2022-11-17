using MarketArea.Constants;
using MarketArea.Contracts;
using MarketArea.Data;
using MarketArea.Data.Common;
using MarketArea.ModelBinders;
using MarketArea.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0; 

})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IAdService, AdService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
    options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
