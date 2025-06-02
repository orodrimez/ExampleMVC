using EPractico_Optim.Data;
using EPractico_Optim.Repository;
using EPractico_Optim.Repository.IRepository;
using EPractico_Optim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICocktailRepository, CocktailRepository>();
builder.Services.AddHttpClient<CocktailApiService>(client =>
{
    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cocktails}/{action=Index}/{id?}");

app.Run();
