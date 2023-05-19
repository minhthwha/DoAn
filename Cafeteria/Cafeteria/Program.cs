using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;

var builder = WebApplication.CreateBuilder(args);

// Add database context.
builder.Services.AddDbContext<CafeteriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CafeteriaContext") ?? throw new InvalidOperationException("Connection string 'CafeteriaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session.
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.IsEssential = true;
});

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
