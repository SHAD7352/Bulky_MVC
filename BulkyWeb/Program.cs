using BookNestWeb.DataAccess.Repository.IRepository;
using BookNestWeb.DataAccess.Repository;
using BookNestWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BookNest.DataAccess.Repository.IRepository;
using BookNest.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using BookNest.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Indentity/Account/Login";
    options.LogoutPath = $"/Indentity/Account/Logout";
    options.AccessDeniedPath = $"/Indentity/Account/AccessDenied";

});
// Register the ICategoryRepository and its implementation in DI container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// these are the middleware of the application
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//check if user id and password is valid and you can also say these are the pipe line of our project

app.UseAuthentication();
app.UseAuthorization();

//app.MapDefaultControllerRoute();

app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
