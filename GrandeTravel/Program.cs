using GrandeTravel.Data;
using GrandeTravel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository<TravelPackage>, BaseRepository<TravelPackage>>();
builder.Services.AddScoped<IRepository<Booking>, BaseRepository<Booking>>();
builder.Services.AddScoped<IRepository<Feedback>, BaseRepository<Feedback>>();
builder.Services.AddScoped<IRepository<TravelProviderProfile>, BaseRepository<TravelProviderProfile>>();
builder.Services.AddScoped<IRepository<CustomerProfile>, BaseRepository<CustomerProfile>>();
builder.Services.AddScoped<IRepository<Photo>, BaseRepository<Photo>>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddIdentity<MyUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
});
var app = builder.Build();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await GTDbSeed.Seed(serviceProvider);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
/*app.MapRazorPages();*/


app.Run();
