using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controller ve View'ların birlikte kullanılacağını ifade ettik
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b=>b.MigrationsAssembly("StoreApp")); //Migrations ifadeleri StoreApp klasörü içerisinde oluşacak
});

builder.Services.AddScoped<IRepositoryManager,RepositoryManager>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>(); // ilgili ifadenin tanımlanmasını gerçekleştirdik

builder.Services.AddScoped<IServiceManager,ServiceManager>(); 
builder.Services.AddScoped<IProductService,ProductManager>(); 
builder.Services.AddScoped<ICategoryService,CategoryManager>(); 


var app = builder.Build();

app.UseStaticFiles();   // Static dosyalara ulaşabilmek için
app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}");

app.Run();
