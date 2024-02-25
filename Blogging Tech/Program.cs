using BLL;
using DAL.Entity;
using DAL.Repos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using DAL;
using AutoMapper;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();  
builder.Services.AddScoped<PostRepo>();
builder.Services.AddAutoMapper(typeof(Program));  
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>(); 
builder.Services.AddSingleton(sp => new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Post, PostVM>();
    cfg.CreateMap<Category, CategoryVM>();
    cfg.CreateMap<User, UserVM>();
}));

builder.Services.AddScoped(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
