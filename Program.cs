using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Repositorys;
using Zadatak1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appResponses.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserTokenService, UserTokenService>();
builder.Services.AddScoped<IResponseMessageService, ResponseMessageService>();
builder.Services.AddScoped<ExceptionHandler>(); 

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<MySeeder>();



builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShopDB;Trusted_Connection=true;");
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ShopContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var seeder = new MySeeder(); 
        await seeder.SeedUser(userManager);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
