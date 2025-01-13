using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TicketBooking.Repositories;
using TicketBooking.Repositories.Implementations;
using TicketBooking.Repositories.Interfaces;
using AutoMapper;
using TicketBooking.Web.Controllers.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Rejestracja DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TicketBooking.Web")
    )
);

// Rejestracja repozytoriów
builder.Services.AddScoped<IKoncertRepo, KoncertRepo>();
builder.Services.AddScoped<IMiejscaDetailsRepo, MiejscaDetailsRepo>();
builder.Services.AddScoped<IUtilityRepo, UtilityRepo>(); 

// Rejestracja IHttpContextAccessor
builder.Services.AddHttpContextAccessor(); 

// Rejestracja AutoMapper
var config = new MapperConfiguration(options =>
{
    options.AddProfile(new AutoMapperProfile()); 
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper); 

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
