using BookNestWeb.DataAccess.Repository.IRepository;
using BookNestWeb.DataAccess.Repository;
using BookNestWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BookNest.DataAccess.Repository.IRepository;
using BookNest.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the ICategoryRepository and its implementation in DI container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
